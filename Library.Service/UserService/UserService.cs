using Library.Data.Entites.BookEntites;
using Library.Data.Entites.IdentityEntities;
using Library.Repository.Interfaces;
using Library.Service.HandleResponses;
using Library.Service.TokenService;
using Library.Service.UserService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Publisher = Library.Data.Entites.BookEntites.Publisher;

namespace Library.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                throw new CustomException(404, "User Not Found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                throw new CustomException(400, "Login Failed");


            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activRefTok = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                user.RefreshToken = activRefTok.Token;
                user.RefreshTokenExpiration = activRefTok.ExpiresOn;
            }
            else
            {
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);

            }
            var userDto = new UserDto
            {
                UserName = user.UserName,
                Email = loginDto.Email,
                Token = _tokenService.GenerateToken(user, role),
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiration,
                IsAdmin = user.IsAdmin
            };
            _tokenService.SetRefreshTokenToCookie(userDto.RefreshToken, userDto.RefreshTokenExpiration);

            return userDto;
        }

        public async Task<UserDto> Register(RegisterDto registerDto, UserRoles UserRoles)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null)
                throw new CustomException(500, "Email Already Exists");

            if (!UserRoles.IsDefined(UserRoles))
                UserRoles = UserRoles.Customer;

            var refreshToken = _tokenService.GenerateRefreshToken();

            var appUser = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                IsAdmin = false,
                UserRoles = UserRoles,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                RefreshTokens = new List<RefreshToken>()
            };

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!result.Succeeded)
                throw new CustomException(400, "Register Failed");


            if (UserRoles == UserRoles.Publisher)
            {
                var publisher = new Publisher
                {
                    Name = registerDto.UserName,
                };
                await _unitOfWork.Repository<Publisher, Guid>().AddAsync(publisher);
                await _unitOfWork.CompleteAsync();
            }
            //Add Roles 
            await _userManager.AddToRoleAsync(appUser, UserRoles.ToString());

            appUser.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(appUser);

            // Generate token
            var userDto = new UserDto
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Token = _tokenService.GenerateToken(appUser, UserRoles.ToString()),
                RefreshToken = appUser.RefreshToken,
                RefreshTokenExpiration = appUser.RefreshTokenExpiration
            };

            _tokenService.SetRefreshTokenToCookie(userDto.RefreshToken, userDto.RefreshTokenExpiration);

            return userDto;
        }

        public async Task RevokeRefreshTokenAsync(string? token = null)
        {
            // لو التوكن مش مبعوت خد من الكوكيز
            if (string.IsNullOrWhiteSpace(token))
            {
                token = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            }

            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "Refresh token is required.");

            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == token));

            if (user == null)
                throw new CustomException(404, "Invalid refresh token.");

            var refreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == token);

            if (refreshToken == null)
                throw new CustomException(404, "Refresh token not found.");

            if (!refreshToken.IsActive)
                throw new CustomException(400, "Refresh token is already revoked.");

            refreshToken.RevokedAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            _httpContextAccessor.HttpContext?
                .Response
                .Cookies
                .Delete("refreshToken");
        }

        public async Task Logout()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                throw new CustomException(404, "there is no logged account");
            }

            await RevokeRefreshTokenAsync(token);
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> RefreshTokenAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new CustomException(404, "No refresh token Exists");

            var user = await _userManager.Users
           .Include(u => u.RefreshTokens)
           .FirstOrDefaultAsync(u =>
               u.RefreshTokens.Any(rt =>
                   rt.Token == token
                   && rt.RevokedAt == null
                   && rt.ExpiresOn > DateTime.UtcNow
               )
           );

            if (user == null)
                throw new CustomException(400, "Invalid or expired refresh token");

            //var oldToken = user.RefreshTokens.Single(rt => rt.Token == token);

            //oldToken.RevokedAt = DateTime.UtcNow;

            //var newToken = _tokenService.RenewRefreshToken(user);

            //user.RefreshTokens.Add(oldToken);
            //await _userManager.UpdateAsync(user);


            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()!;
            var accessToken = _tokenService.GenerateToken(user, role);

            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = accessToken,
                //RefreshToken = newToken.Token,
                RefreshTokenExpiration = user.RefreshTokenExpiration
            };
        }
    }
}
// add enum of roles , add roles as a list 