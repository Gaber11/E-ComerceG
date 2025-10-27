
namespace Persistance.Data.Configrations
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);  
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,3)");
        }
    }
}
