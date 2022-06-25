using ECommerce.Application.Mappers;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Responses;
using ECommerce.Core.ViewModels;
using ECommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, ProductMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Response<List<ProductVM>> Get()
        {
            return new Response<List<ProductVM>>
            {
                Data = _mapper.MapFromSourceToDestination(
                    _unitOfWork.ProductRepository.All().ToList())
            };
        }

        public Response<List<ProductVM>> Get(Expression<Func<Product, bool>> query)
        {
            return new Response<List<ProductVM>>
            {
                Data = _mapper.MapFromSourceToDestination(
                _unitOfWork.ProductRepository.Filter(query).ToList())
            };
        }

        public Response<ProductVM> Get(int Id)
        {
            return new Response<ProductVM>
            {
                Data = _mapper.MapFromSourceToDestination(
                _unitOfWork.ProductRepository.FirstOrDefault())
            };
        }
    }
}
