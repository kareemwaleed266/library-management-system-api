using AutoMapper;
using Library.Data.Entites;
using Library.Data.Entites.BookEntites;
using Library.Data.Entites.IdentityEntities;
using Library.Repository.Interfaces;
using Library.Service.HandleResponses;
using Library.Service.TransactionService.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly EncryptionHelper _encryptionHelper;

        public TransactionService(
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            IMapper mapper,
            EncryptionHelper encryptionHelper
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _encryptionHelper = encryptionHelper;
        }
        public async Task<TransactionResultDto> BorrowBook(TransactionDto transaction, int id)
        {
            var user = await _userManager.FindByEmailAsync(transaction.UserEmail);

            if (user == null)
                throw new CustomException(404, "User Not Found");

            var book = await _unitOfWork.Repository<Book, int>().GetIfExistsAsync(book => book.Id == id);

            if (book == null)
                throw new CustomException(404, "Book Not Found");

            if (book.BookStatus == BookStatus.Borrowed)
                throw new CustomException(400, $"Book Is Borrowed To another User and its end date is {DateTime.Now.AddDays(14)} (14 Days)");

            if (book.BookStatus == BookStatus.SoldOut)
                throw new CustomException(400, $"Book Is Sold Out");

            var mappedTransaction = new TransactionResultDto
            {
                BookId = book.Id,
                BookTitle = book.Title,
                UserId = user.Id,
                UserName = user.UserName,
                Price = book.Price
            };

            var newTransaction = _mapper.Map<Transaction>(mappedTransaction);
            newTransaction.TransStatus = TransStatus.succeeded;

            await _unitOfWork.Repository<Transaction, Guid>().AddTransAsync(newTransaction);
            await _unitOfWork.CompleteIdentityAsync();

            book.BookStatus = BookStatus.Borrowed;
            await _unitOfWork.CompleteAsync();

            return mappedTransaction;
        }

        public async Task<TransactionResultDto> BuyBook(TransactionDto transaction, int id)
        {
            var user = await _userManager.FindByEmailAsync(transaction.UserEmail);

            if (user == null)
                throw new CustomException(404, "User Not Found");

            var book = await _unitOfWork.Repository<Book, int>().GetIfExistsAsync(book => book.Id == id);

            if (book == null)
                throw new CustomException(404, "Book Not Found");

            if (book.BookStatus == BookStatus.SoldOut)
                throw new CustomException(400, "Book Is Sold");

            if (book.BookStatus == BookStatus.Borrowed)
                throw new CustomException(400, $"Book Is Borrowed To another User and its end date is {DateTime.Now.AddDays(14)} (14 Days)");


            var mappedTransaction = new TransactionResultDto
            {
                BookId = book.Id,
                BookTitle = book.Title,
                UserId = user.Id,
                UserName = user.UserName,
                Price = book.Price
            };

            var newTransaction = _mapper.Map<Transaction>(mappedTransaction);
            newTransaction.TransStatus = TransStatus.succeeded;

            await _unitOfWork.Repository<Transaction, Guid>().AddTransAsync(newTransaction);
            await _unitOfWork.CompleteIdentityAsync();

            book.BookStatus = BookStatus.SoldOut;
            await _unitOfWork.CompleteAsync();

            return mappedTransaction;
        }
    }
}
