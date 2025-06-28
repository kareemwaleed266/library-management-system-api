using Library.Data.Entites.BookEntites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.BookService.Dtos
{
    public class BookDto
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
        public string PublisherName { get; set; }
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
