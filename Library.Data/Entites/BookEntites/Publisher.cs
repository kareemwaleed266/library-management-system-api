using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entites.BookEntites
{
    public class Publisher :BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
