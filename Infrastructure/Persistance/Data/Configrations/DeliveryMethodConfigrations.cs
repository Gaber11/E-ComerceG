using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configrations
{
    public class DeliveryMethodConfigrations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(18,3)");



        }
    }
}
