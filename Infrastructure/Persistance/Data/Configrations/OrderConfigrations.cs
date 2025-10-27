using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{

    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, s => s.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.PaymentStatus).HasConversion(PaymentStatus 
                => PaymentStatus.ToString(), 
                s => Enum.Parse<OrderPaymentStatus>(s));
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,3)");
        }
    }
}
