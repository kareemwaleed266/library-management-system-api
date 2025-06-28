using Library.Data.Entites.BookEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //builder.HasOne(Book => Book.Category)
            //    .WithOne().OnDelete(DeleteBehavior.Cascade);
            
            //builder.HasOne(Book => Book.Publisher)
            //    .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
