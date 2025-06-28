using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Entites.BookEntites;

namespace Library.Service.TransactionService.Dtos
{
    public class TransactionResultDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public decimal Price { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}
