using Library.Service.TransactionService.Dtos;

namespace Library.Service.TransactionService
{
    public interface ITransactionService
    {
        Task<TransactionResultDto> BorrowBook(TransactionDto transaction,int id);
        Task<TransactionResultDto> BuyBook(TransactionDto transaction, int id);
    }
}