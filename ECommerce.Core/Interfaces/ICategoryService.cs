using ECommerce.Core.Responses;
using ECommerce.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface ICategoryService
    {
        Response<List<CategoryVM>> Get();

    }
}
