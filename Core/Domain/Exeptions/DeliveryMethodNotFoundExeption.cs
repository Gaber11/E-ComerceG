using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class DeliveryMethodNotFoundExeption(int id): NotFoundExeption($"The Delivery Method With {id} is not Found")
    {

    }
}
