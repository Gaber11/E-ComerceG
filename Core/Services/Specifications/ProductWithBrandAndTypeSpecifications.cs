
namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        //use to retrieve product by id
        public ProductWithBrandAndTypeSpecifications(int id) : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters)
            : base(product=>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim()))

            )
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {

                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;

                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    case ProductSortOptions.NameAsc:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }
            ApplyPagination(parameters.PageIndex, parameters.PageSize);

        }

    }
}
