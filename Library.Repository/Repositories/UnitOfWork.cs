//using Library.Data.Context;
//using Library.Data.Entites;
//using Library.Repository.Interfaces;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Library.Repository.Repositories
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        private readonly LibraryManagementSystemDbContext _context;
//        private readonly LibraryManagementSystemIdentityDbContext _identityDbContext;
//        private Hashtable _repositories;
//        public UnitOfWork(LibraryManagementSystemDbContext context, LibraryManagementSystemIdentityDbContext identityDbContext)
//        {
//            _context = context;
//            _identityDbContext = identityDbContext;
//        }
//        public async Task<int> CompleteAsync()
//            => await _context.SaveChangesAsync();
//        public async Task<int> CompleteIdentityAsync()
//            => await _identityDbContext.SaveChangesAsync();

//        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
//        {
//            if (_repositories == null)
//                _repositories = new Hashtable();

//            var entityKey = typeof(TEntity).Name;
//            //if(entityKey == "Transaction")
//            //{
//            //    if (!_repositories.ContainsKey(entityKey))
//            //    {
//            //        var repositoryType = typeof(GenericRepository<,>);
//            //        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _identityDbContext);
//            //        _repositories.Add(entityKey, repositoryInstance);
//            //    }
//            //    return ((IGenericRepository<TEntity, TKey>)_repositories[entityKey]);
//            //}

//            if (!_repositories.ContainsKey(entityKey))
//            {
//                //var repositoryType = typeof(GenericRepository<,>);
//                //var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
//                var repositoryType = typeof(GenericRepository<,>);
//                var constructorParams = entityKey == "Transaction" ? new object[] { _identityDbContext } : new object[] { _context };

//                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), constructorParams);
//                _repositories.Add(entityKey, repositoryInstance);
//            }
//            return ((IGenericRepository<TEntity, TKey>)_repositories[entityKey]);
//        }
//    }
//}
using System;
using System.Collections;
using System.Threading.Tasks;
using Library.Data.Context;
using Library.Data.Entites;
using Library.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryManagementSystemDbContext _context;
        private readonly LibraryManagementSystemIdentityDbContext _identityDbContext;
        private Hashtable _repositories;

        public UnitOfWork(LibraryManagementSystemDbContext context, LibraryManagementSystemIdentityDbContext identityDbContext)
        {
            _context = context;
            _identityDbContext = identityDbContext;
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Exception Stack Trace: {ex.InnerException.StackTrace}");
                }

                throw;
            }
        }

        public async Task<int> CompleteIdentityAsync()
            => await _identityDbContext.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var entityKey = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepository<,>);
                object[] constructorParams;

                if (entityKey == "Transaction")
                {
                    constructorParams = new object[] { _identityDbContext };
                }
                else
                {
                    constructorParams = new object[] { _context };
                }

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), constructorParams);
                _repositories.Add(entityKey, repositoryInstance);
            }

            return (IGenericRepository<TEntity, TKey>)_repositories[entityKey];
        }

        public void DetachEntity<TEntity, TKey>(TEntity entity) where TEntity : BaseEntity<TKey>
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }

}