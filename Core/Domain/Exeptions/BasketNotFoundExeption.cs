using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class BasketNotFoundExeption: NotFoundExeption
    {

        public BasketNotFoundExeption(string Id) : base($"Basket with id : {Id} is  not found")
        {

        }
    }
}
