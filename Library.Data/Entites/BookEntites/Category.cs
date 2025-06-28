using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entites.BookEntites
{
    public class Category :BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
