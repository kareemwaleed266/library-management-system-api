using Library.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Interfaces
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        Task AddTransAsync(Transaction entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<Transaction>> GetAllTransAsync();
        Task <TEntity> GetByIdAsync(TKey? id);
        // used in book and transaction for its status
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetIfExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> GetAllIfExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetIdentityIfExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IdentityExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
