using AutoMapper;
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
    public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, CategoryMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Response<List<CategoryVM>> Get()
        {
            return new Response<List<CategoryVM>> {Data = _mapper.MapFromSourceToDestination(
                    _unitOfWork.CategoryRepository.All().ToList()) };
        }

        public Response<List<CategoryVM>> Get(Expression<Func<Category, bool>> query)
        {
            return new Response<List<CategoryVM>>
            {
                Data = _mapper.MapFromSourceToDestination(
                _unitOfWork.CategoryRepository.Filter(query).ToList())
            };
        }

        public Response<CategoryVM> Get(int Id)
        {
            return new Response<CategoryVM>
            {
                Data = _mapper.MapFromSourceToDestination(
                _unitOfWork.CategoryRepository.FirstOrDefault())
            };
        }
    }
}
