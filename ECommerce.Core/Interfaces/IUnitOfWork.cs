using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<Product> ProductRepository { get; }
        bool Commit();

        Task<int> CommitAsync();
    }
}
