using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.TransactionService.Dtos
{
    public class TransactionDto
    {
        [EmailAddress]
        [Required]
        public string UserEmail { get; set; }
    }
}
