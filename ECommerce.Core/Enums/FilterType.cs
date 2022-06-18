using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Enums
{
    public enum FilterType
    {
        Equals = 0,
        NotEquals = 1,
        Between = 2,
        GreaterOrEquals = 3,
        GreaterThan = 4,
        LessOrEquals = 5,
        LessThan = 6,
        Like = 8,
        NotLike = 9,
        IsNull = 10,
    }
}
