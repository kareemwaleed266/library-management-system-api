using AutoMapper;
using Library.Data.Entites.BookEntites;
using Library.Data.Entites.IdentityEntities;
using Library.Repository.Interfaces;
using Library.Service.BookService.Dtos;
using Library.Service.HandleResponses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly EncryptionHelper _encryptionHelper;

        public BookService(
           IUnitOfWork unitOfWork,
           IMapper mapper,
           UserManager<AppUser> userManager,
           EncryptionHelper encryptionHelper
        )

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _encryptionHelper = encryptionHelper;
        }

        private async Task<bool> CheckBookDetails(BookDto details)
        {
            var categoryExist = await _unitOfWork.Repository<Category, int>().ExistsAsync(category => category.Name == details.CategoryName);

            if (!categoryExist)
            {
                var category = new Category { Name = details.CategoryName };
                await _unitOfWork.Repository<Category, int>().AddAsync(category);
                await _unitOfWork.CompleteAsync();
            }

            var publisherExist = _unitOfWork.Repository<Publisher, Guid>().ExistsAsync(publisher => publisher.Name == details.PublisherName);

            if (!await publisherExist)
                throw new CustomException(400, "Publisher Not Exist");

            return true;
        }

        public async Task<BookResultDto> AddBookAsync(BookDto input)
        {
            if (input == null) throw new ArgumentNullException("input");

            var user = await _userManager.FindByEmailAsync(input.UserEmail);
            if (user == null)
                throw new CustomException(400, "User Not Found");

            if (!user.IsAdmin)
            {
                throw new CustomException(401, "You are not allowed to do this action");
            }

            var bookExist = await _unitOfWork.Repository<Book, int>().GetIfExistsAsync(book => book.Title == input.Title);
            if (bookExist != null)
            {
                throw new CustomException(400, "Book Already Exists");
            }


            var result = CheckBookDetails(input);
            if (!await result)
            {
                await AddBookAsync(input);
            }

            input.Author = _encryptionHelper.Encrypt(input.Author);

            var book = _mapper.Map<Book>(input);

            await _unitOfWork.Repository<Book, int>().AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookResultDto>(book);
        }

        public async Task DeleteBook(int? bookId)
        {
            if (!bookId.HasValue)
                throw new ArgumentNullException(nameof(bookId));

            var book = await _unitOfWork.Repository<Book, int>().GetByIdAsync(bookId.Value);

            if (book == null)
                throw new CustomException(404, $"Book with ID {bookId.Value} not found.");

            _unitOfWork.Repository<Book, int>().Delete(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IReadOnlyList<BookResultDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.Repository<Book, int>().GetAllAsync();


            foreach (var book in books)
            {
                book.Author = _encryptionHelper.Decrypt(book.Author);
            }

            return _mapper.Map<IReadOnlyList<BookResultDto>>(books);
        }

        public async Task<BookResultDto> GetBookByIdAsync(int? bookId)
        {
            var book = await _unitOfWork.Repository<Book, int>().GetByIdAsync(bookId.Value);

            if (book == null)
                throw new CustomException(404, "Book Not Found");

            book.Author = _encryptionHelper.Decrypt(book.Author);

            return _mapper.Map<BookResultDto>(book);
        }

        public async Task<BookResultDto> UpdateBookAsync(int? bookId, BookDto input)
        {
            if (input == null) throw new ArgumentNullException("input");

            var book = await _unitOfWork.Repository<Book, int>().GetByIdAsync(bookId.Value);

            if (book == null)
                throw new CustomException(404, "Book Not Found");


            var user = await _userManager.FindByEmailAsync(input.UserEmail);
            if (user == null)
                throw new CustomException(404, "User Not Found");

            var bookExist = await _unitOfWork.Repository<Book, int>().GetIfExistsAsync(book => book.Title == input.Title);
            if (bookExist != null)
            {
                throw new CustomException(400, "Book Already Exists");
            }


            var result = CheckBookDetails(input);
            if (!await result)
            {
                await UpdateBookAsync(book.Id, input);
            }


            input.Author = _encryptionHelper.Encrypt(input.Author);


            _unitOfWork.DetachEntity<Book, int>(book);

            var addedBook = _mapper.Map<Book>(input);

            addedBook.Id = bookId.Value;

            _unitOfWork.Repository<Book, int>().Update(addedBook);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookResultDto>(addedBook);
        }
    }
}
