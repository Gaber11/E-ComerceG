using Domain.Exeptions;
using Shared.Dtos;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<PagenatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters)
        {
            //retrieve all PRODUCTS from the database ==> unitofwork
            //mapping results to BrandResultDto ==> automapper IMapper
            var products = await _unitOfWork.GetGenericRepo<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var TotalCount = await _unitOfWork.GetGenericRepo<Product, int>().CountAsync(new ProductCountSpecifications(parameters));

            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            //return productResult;
            var Result = new PagenatedResult<ProductResultDto>(
             productsResult.Count(),
             parameters.PageIndex,
           TotalCount,

             productsResult);
            return Result;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            //retrieve all brands from the database ==> unitofwork
            //mapping results to BrandResultDto ==> automapper IMapper
            //return the result
            var brands = await _unitOfWork.GetGenericRepo<ProductBrand, int>().GetAllAsync();
            var brandResult = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandResult;
        }

     

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            //retrieve all types from the database ==> unitofwork
            //mapping results to typesResultDto ==> automapper IMapper
            //return the result
            var types = await _unitOfWork.GetGenericRepo<ProductType, int>().GetAllAsync();
            var typeResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typeResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetGenericRepo<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            //var productResult = _mapper.Map<ProductResultDto>(product);
            //return productResult;
            return product is null ? throw new ProductNotFoundExeption(id) : _mapper.Map<ProductResultDto>(product);

        }
    }
}
