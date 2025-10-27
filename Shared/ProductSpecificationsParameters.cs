using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationsParameters
    {

        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortOptions? Sort { get; set; }
        public string? Search { get; set; }

        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _pageSize = DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }



    }
    public enum ProductSortOptions
    {
        NameAsc ,
        NameDesc ,
        PriceAsc,
        PriceDesc
    }
}
