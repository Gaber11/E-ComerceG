using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class OrderNotFoundExeption(Guid id) : NotFoundExeption ($"The Order With Id {id} is not Found")
    {
        
    }
}

