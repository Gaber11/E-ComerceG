using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderRequest
    {
        public string BasketId { get; init; }
        public AddressDto ShipToAddress { get; init; }
        public int DeliveryMethodId { get; init; }

    }
}
