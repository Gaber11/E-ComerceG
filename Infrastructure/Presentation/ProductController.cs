

using Microsoft.AspNetCore.Authorization;
using Shared.Dtos;
using Shared.ErrorModel;
using System.Net;

namespace Presentation
{
    //[Authorize] // Login

    public class ProductsController(IServiceManager ServiceManager) : ApiController
    {
        [RedisCache]
        [HttpGet]
        //parameters ==> brandId, typeId, sort, pageIndex, pageSize
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters) // Action can return a result of type IEnumerable<ProductResultDto>
        {
            var products = await ServiceManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands() // Action can return a result of type IEnumerable<BrandsResultDto>
        {
            var brands = await ServiceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes() // Action can return a result of type IEnumerable<TypesResultDto>
        {
            var types = await ServiceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.OK)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int id) // Action can return a result of type IEnumerable<TypesResultDto>
        {
            var product = await ServiceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
    }
}

