using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rocket.Core.Entities;
using Rocket.Core.Repository;
using Rocket.Core.Specification;
using Rocket.DTOS;
using Rocket.Errors;
using Rocket.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocket.Controllers
{
 
    public class ProductController : BaseController
    {
        private readonly IGenericRepository<Product> _productRep;
        private readonly IMapper _map;
        private readonly IGenericRepository<ProductBrand> _brandrepo;
        private readonly IGenericRepository<ProductType> _typerepo;

        public ProductController(
            IGenericRepository<Product> ProductRep ,
            IMapper map , 
            IGenericRepository<ProductBrand> brandrepo , 
            IGenericRepository<ProductType> typerepo)
        {
            _productRep = ProductRep;
            _map = map;
            _brandrepo = brandrepo;
            _typerepo = typerepo;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllAsync([FromQuery]ProductSpecParameter ProductParamete)
        {
            var spec=new ProductWithBrandandTypeSpecification(ProductParamete);
            var products = await _productRep.GetAllwithspecAsync(spec);
            var Data = _map.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            var countspec= new ProductWithFilterForCountSpecification(ProductParamete);
            var count= await _productRep.GetCountAsync(countspec);
            return Ok(new Pagination<ProductToReturnDto>(ProductParamete.PageIndex , ProductParamete.PageSize , count , Data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandandTypeSpecification(id);
            var product = await _productRep.GetByIdwithspecAsync(spec);
            if (product == null) return NotFound(new ApiResponse(400));

         
            return Ok(_map.Map<Product,ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAllBrands()
        {
            var brands= await _brandrepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllTypes()
        {
            var types = await _typerepo.GetAllAsync();
            return Ok(types);
        }
    }
} 
