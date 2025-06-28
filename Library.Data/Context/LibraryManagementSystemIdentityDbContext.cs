using Library.Data.Entites;
using Library.Data.Entites.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Context
{
    public class LibraryManagementSystemIdentityDbContext : IdentityDbContext<AppUser, AppRoles, string>
    {
        public LibraryManagementSystemIdentityDbContext(DbContextOptions<LibraryManagementSystemIdentityDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
