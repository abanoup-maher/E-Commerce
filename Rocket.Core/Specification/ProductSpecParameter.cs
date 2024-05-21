using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Core.Specification
{
    public class ProductSpecParameter
    {
        private int pageSize = 5;
        public int PageIndex { get; init; } = 1;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value> 10? 10 : value ; }
        }
        private string Search;

        public string search
        {
            get { return Search; }
            set { Search = value.ToLower(); }
        }


        public string sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
    }
}
