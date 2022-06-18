using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class PagedList<T>
    {
        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> collection, int totalCount)
        {
            Collection = collection;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Collection { get; set; }

        public int TotalCount { get; set; }
    }
}
