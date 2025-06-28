using Library.Data.Context;
using Library.Data.Entites;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly LibraryManagementSystemDbContext _context;
        private readonly LibraryManagementSystemIdentityDbContext _identityDbContext;

        //public GenericRepository(LibraryManagementSystemDbContext context,LibraryManagementSystemIdentityDbContext identityDbContext)
        //{
        //    _context = context;
        //    _identityDbContext = identityDbContext;
        //}
        public GenericRepository(LibraryManagementSystemDbContext context)
        {
            _context = context;
        }

        public GenericRepository(LibraryManagementSystemIdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);
        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().AnyAsync(predicate);
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();
        public async Task<IReadOnlyList<TEntity>> GetAllIfExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().Where(predicate).ToListAsync();
        public async Task<TEntity> GetByIdAsync(TKey? id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetIfExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public async Task AddTransAsync(Transaction entity)
            => await _identityDbContext.Set<Transaction>().AddAsync(entity);

        public async Task<bool> IdentityExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _identityDbContext.Set<TEntity>().AnyAsync(predicate);

        public async Task<IReadOnlyList<Transaction>> GetAllTransAsync()
            => await _identityDbContext.Set<Transaction>().ToListAsync();


        public async Task<TEntity> GetIdentityIfExistsAsync(Expression<Func<TEntity, bool>> predicate)
            => await _identityDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);


    }
}
