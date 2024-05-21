using Rocket.Core.Entities;
using Rocket.Core.Repository;
using Rocket.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RocketStoreDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(RocketStoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        =>
            await _context.SaveChangesAsync();
        

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository  <TEntity>() where TEntity : BaseEntity
        {
            if(_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type=typeof(TEntity).Name;
            
            if(!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return  (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
