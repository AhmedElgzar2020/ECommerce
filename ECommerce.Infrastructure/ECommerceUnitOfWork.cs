﻿using ECommerce.Core.Interfaces;
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
        private readonly DbContext _context;

        public ECommerceUnitOfWork(DbContext context)
        {
            _context = context;
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