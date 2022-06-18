using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Search
{
    public class Paging
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public bool AllowPaging { get; set; }
    }
}
