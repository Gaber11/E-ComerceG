using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public sealed class UserNotFoundException(string email) : NotFoundExeption($"The User with Email {email} is not Found")
    {
    }
}
