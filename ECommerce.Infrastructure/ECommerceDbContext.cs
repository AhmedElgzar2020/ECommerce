using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public class ECommerceDbContext: DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }
    }
}
