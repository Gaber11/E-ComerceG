using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class ProductNotFoundExeption: NotFoundExeption
    {
        public ProductNotFoundExeption(int id) : base($"Product with {id} Not Found")
        {
            
        }
    }
}
