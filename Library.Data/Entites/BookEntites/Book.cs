using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entites.BookEntites
{
    public enum BookStatus
    {
        SoldOut,
        Exists,
        Borrowed
    }
    public class Book :BaseEntity<int>
    {
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string Author { get; set; }
        public string PublisherName { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;

        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }

        [EnumDataType(typeof(BookStatus))]
        public BookStatus BookStatus { get; set; } = BookStatus.Exists;
    }
}
