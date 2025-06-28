using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Library.Data.Entites.IdentityEntities
{
    public enum UserRoles
    {
        Customer = 1,
        Publisher = 2
    }
    public enum Roles
    {
        Admin = 3,
    }
    public class AppUser : IdentityUser
    {
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public UserRoles UserRoles { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
