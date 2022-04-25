using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaximumPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int pageSize = 6;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaximumPageSize) ? MaximumPageSize : value; }
        }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }

        private string search;
        public string Search
        {
            get => search;
            set => search = value.ToLower();
        }
    }
}
