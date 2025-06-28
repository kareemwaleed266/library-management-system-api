using Library.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task <int> CompleteAsync();
        Task<int> CompleteIdentityAsync();
        IGenericRepository<TEntity,TKey> Repository<TEntity ,TKey>() where TEntity : BaseEntity<TKey>;
        void DetachEntity<TEntity, TKey>(TEntity entity) where TEntity : BaseEntity<TKey>;
    }
}
