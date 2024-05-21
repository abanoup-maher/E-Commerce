using Microsoft.EntityFrameworkCore;
using Rocket.Core.Entities;
using Rocket.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Repository.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery, ISpecification<TEntity> spec)
        {
            var query = inputquery;


            if (spec.Criateria != null)
            {
                query = query.Where(spec.Criateria);
            }
            if(spec.OrderByDesc != null)
            {
                query=query.OrderByDescending(spec.OrderByDesc);
            }
            if(spec.OrderBy!=null)
            {
                query=query.OrderBy(spec.OrderBy);
            }

            if(spec.IsPAginationEnable)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (currentquery, includeExp) => currentquery.Include(includeExp));
            return query;

        }
    }


}
