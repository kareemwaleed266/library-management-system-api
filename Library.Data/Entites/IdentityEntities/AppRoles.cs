using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entites.IdentityEntities
{
    public class AppRoles : IdentityRole
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
