using Rocket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Specification
{
    public class ProductWithBrandandTypeSpecification : BaseSpecification<Product>
    {
        //for get all products
        public ProductWithBrandandTypeSpecification(ProductSpecParameter ProductParameter) : base(
          p =>
          (string.IsNullOrEmpty(ProductParameter.search) || p.Name.ToLower().Contains(ProductParameter.search))
          &&
          (!ProductParameter.BrandId.HasValue || p.ProductBrandId == ProductParameter.BrandId.Value) 
          &&
              (!ProductParameter.TypeId.HasValue || p.ProductTypeId == ProductParameter.TypeId.Value)
          )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            ApplyPagination(ProductParameter.PageSize * (ProductParameter.PageIndex - 1),ProductParameter.PageSize);

            if (!string.IsNullOrEmpty(ProductParameter.sort))
            {
                switch (ProductParameter.sort)
                {
                    case "PriceASC": AddOrderBy(p => p.Price); break;
                    case "PriceDESC": AddOrderByDesc(p => p.Price); break;
                    default: AddOrderBy(p => p.Name); break;
                }
            }


        }

        //to get specific product
        public ProductWithBrandandTypeSpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
