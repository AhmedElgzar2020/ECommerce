using ECommerce.Core.Interfaces;
using ECommerce.Core.Responses;
using ECommerce.Core.ViewModels;
using ECommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ECommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public Response<List<CategoryVM>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
