using ECommerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Search
{
    public class Sorting
    {
        public string Field { get; set; }

        public SortingDirection Dir { get; set; }
    }

}
