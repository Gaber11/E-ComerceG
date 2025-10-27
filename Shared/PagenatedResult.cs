using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PagenatedResult<TData>(int pageIndex, int pageSize, int totalCount, IEnumerable<TData> Data)
    {

    }
}
