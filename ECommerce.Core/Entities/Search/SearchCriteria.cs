using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Search
{
    public class SearchCriteria
    {
        public IList<FilterCriteria> FilterCriteria { get; set; } = new List<FilterCriteria>();

        public Sorting SortCriteria { get; set; }

        public Paging PageCriteria { get; set; }
    }
}
