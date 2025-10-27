
namespace Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        //id
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        //Navigational property one {productbrand}
        public ProductBrand ProductBrand { get; set; }
        public int BrandId { get; set; }
        //Navigational property one {producttype}
        public ProductType ProductType { get; set; }
        public int TypeId { get; set; }
    }
}
