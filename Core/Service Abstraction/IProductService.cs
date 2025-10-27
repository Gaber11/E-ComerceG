using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceAbstraction
{
    public interface IProductService
    {
        //Get all products
        public Task<PagenatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
        //Get product by id
        public Task<ProductResultDto> GetProductByIdAsync(int id);
        //Get all brands
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //Get all types
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
