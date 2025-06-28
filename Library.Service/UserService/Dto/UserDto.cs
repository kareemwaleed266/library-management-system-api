using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Service.UserService.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public bool IsAdmin { get; set; } 

    }
}
