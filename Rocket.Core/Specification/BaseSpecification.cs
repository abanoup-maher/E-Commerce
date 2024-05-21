using Rocket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public Expression<Func<T, bool>> Criateria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsPAginationEnable { get; set; }
         
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> _criateria)
        {
            Criateria = _criateria;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPAginationEnable = true;
            Skip = skip;
            Take = take;
        }
    }
}
