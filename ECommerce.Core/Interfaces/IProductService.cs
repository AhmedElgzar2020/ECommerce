using ECommerce.Core.Entities;
using ECommerce.Core.Responses;
using ECommerce.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IProductService
    {
        Response<List<ProductVM>> Get();
        Response<List<ProductVM>> Get(Expression<Func<Product, bool>> query);
        Response<ProductVM> Get(int Id);
        bool ProductExists(Expression<Func<Product, bool>> query);
    }
}
