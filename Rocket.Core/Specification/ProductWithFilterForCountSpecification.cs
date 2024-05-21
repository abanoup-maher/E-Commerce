using Rocket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Specification
{
    public class ProductWithFilterForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParameter productparameter):base(
           p=>
           (string.IsNullOrEmpty(productparameter.search) || p.Name.ToLower().Contains(productparameter.search))
           &&
           (!productparameter.BrandId.HasValue || p.ProductBrandId==productparameter.BrandId.Value)
           &&
           (!productparameter.TypeId.HasValue || p.ProductTypeId==productparameter.TypeId.Value))
        {
            
        }
    }
}
