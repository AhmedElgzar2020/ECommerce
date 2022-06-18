using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IUnitOfWork
    {
        bool Commit();

        Task<int> CommitAsync();
    }
}
