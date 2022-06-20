using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(ECommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
    }
}
