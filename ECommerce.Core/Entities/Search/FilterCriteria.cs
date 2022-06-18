using ECommerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Search
{
    public class FilterCriteria
    {
        #region #Constractors#

        public FilterCriteria(string filterName, FilterType filterType, object filterValue, object filterValue2 = null, string filterDataType = null)
        {
            FilterName = filterName;
            FilterType = filterType;
            FilterValue = filterValue;
            FilterValue2 = filterValue2;
            FilterDataType = filterDataType;
        }

        public FilterCriteria()
        {
        }

        #endregion #Constractors#

        #region #Property#

        public string FilterName { get; set; }

        public FilterType FilterType { get; set; }

        public object FilterValue { get; set; }

        public object FilterValue2 { get; set; }

        public string FilterDataType { get; set; }

        public LogicalOperator LogicalOperator { get; set; }

        public FilterCriteria NestedFilterCriteria { get; set; }

        #endregion #Property#
    }
}
