using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure
{
    public class ECommerceUnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;
        private IBaseRepository<Category> _CategoryRepository;

        public ECommerceUnitOfWork(ECommerceDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<Category> CategoryRepository
        {
            get
            {
                if (this._CategoryRepository == null)
                {
                    this._CategoryRepository = new BaseRepository<Category>(_context);
                }

                return _CategoryRepository;
            }
        }
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}
