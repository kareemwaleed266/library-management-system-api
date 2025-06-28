using Library.Data.Entites.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.UserService.Dto
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).*$", ErrorMessage = "Error: Password must contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
