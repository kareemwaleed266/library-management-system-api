        using Library.Service.TransactionService;
using Library.Service.TransactionService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [Authorize]
        [HttpPost("BorrowBook/{bookId}")]
        public async Task<ActionResult<TransactionResultDto>> BorrowBook(TransactionDto transaction, int bookId)
        {
            var trans = await _transactionService.BorrowBook(transaction,bookId);

            if (trans == null)
                return BadRequest("Error On Borrowing Book");

            return Ok(trans);
        }
        

        [Authorize]
        [HttpPost("BuyBook/{bookId}")]
        public async Task<ActionResult<TransactionResultDto>> BuyBook(TransactionDto transaction,int bookId)
        {
            var trans = await _transactionService.BuyBook(transaction,bookId);

            if (trans == null)
                return BadRequest("Error On Buying Book");

            return Ok(trans);
        }
    }
}
