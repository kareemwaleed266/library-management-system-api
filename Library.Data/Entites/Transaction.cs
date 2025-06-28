using Library.Data.Entites.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entites
{
    public enum TransStatus
    {
        Failed,
        succeeded,
        pending,
        cancelled
    }
    public class Transaction : BaseEntity<Guid>
    {
        public int BookId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public TransStatus TransStatus { get; set; } = TransStatus.pending;
    }
}
