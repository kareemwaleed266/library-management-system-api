using Library.Service.BookService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.BookService
{
    public interface IBookService
    {
        Task<BookResultDto> AddBookAsync(BookDto input);
        Task<BookResultDto> UpdateBookAsync(int? bookId, BookDto input);
        Task DeleteBook(int? bookId);
        Task<BookResultDto> GetBookByIdAsync(int? bookId);
        Task<IReadOnlyList<BookResultDto>> GetAllBooksAsync();
    }
}
