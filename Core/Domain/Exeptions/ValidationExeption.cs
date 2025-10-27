using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeptions
{
    public class ValidationExeption : Exception
    {
        public IEnumerable<string> Errors { get; }
        public ValidationExeption(IEnumerable<string>errors): base("Validation Failed")
        {
            Errors = errors;
        }
    }
}
