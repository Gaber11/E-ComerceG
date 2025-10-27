using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : Specifications<Order>
    {
        //Get order by Id 
        
        public OrderWithIncludeSpecifications(Guid id)
            : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
           // AddInclude(o => o.ShippingAddress);
        }
        //Get all orders for user By email
        public OrderWithIncludeSpecifications(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            SetOrderBy(o => o.OrderDate);

        }
    }
}
