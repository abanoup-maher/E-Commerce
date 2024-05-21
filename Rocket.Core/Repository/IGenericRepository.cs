using Rocket.Core.Entities;
using Rocket.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task <IReadOnlyList<T>> GetAllAsync();
        Task <T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllwithspecAsync(ISpecification<T> spec);
        Task<T> GetByIdwithspecAsync(ISpecification<T> spec);
        Task<int> GetCountAsync(ISpecification<T> spec);

        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
