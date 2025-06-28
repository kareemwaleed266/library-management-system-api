using Library.Data.Entites.IdentityEntities;
using Library.Service.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.UserService
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto registerDto, UserRoles userRoles);
        Task<UserDto> Login(LoginDto loginDto);
        Task Logout();
        Task<UserDto> RefreshTokenAsync();
        Task RevokeRefreshTokenAsync(string? token);
    }
}
