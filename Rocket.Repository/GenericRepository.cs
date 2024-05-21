using Microsoft.EntityFrameworkCore;
using Rocket.Core.Entities;
using Rocket.Core.Repository;
using Rocket.Core.Specification;
using Rocket.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly RocketStoreDbContext _context;

        public GenericRepository(RocketStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if(typeof(T)==typeof(Product))
            //{
            //    return (IEnumerable<T>) await _context.Set<Product>().Include(p=>p.ProductBrand).Include(p=>p.ProductType).ToListAsync();
            //}

             return await _context.Set<T>().ToListAsync();
        }

        
        public async Task<T> GetByIdAsync(int id)
        =>
         // await _context.Set<Product>().Where(p=>p.Id==id).Include(p=>p.ProductBrand).Include(p=>p.ProductType).ToListAsync();
        // await _context.Set<T>().Where(i=>i.Id == id).FirstOrDefaultAsync();
        await _context.Set<T>().FindAsync(id);


        public async Task<IEnumerable<T>> GetAllwithspecAsync(ISpecification<T> spec)
        {
            return await Applyspecification(spec).ToListAsync();
        }


        public async Task<T> GetByIdwithspecAsync(ISpecification<T> spec)
        {
            return await Applyspecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> Applyspecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(),spec);
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await Applyspecification(spec).CountAsync();  
        }

        public async Task CreateAsync(T entity)
        =>
            await _context.Set<T>().AddAsync(entity);


        public void Update(T entity)
       =>
             _context.Set<T>().Update(entity);


        public void Delete(T entity)
        =>
            _context.Set<T>().Remove(entity);
    }
}
