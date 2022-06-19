using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mappers
{
    public class CategoryMapper : MapperBase<Category, CategoryVM>
    {
        private readonly IMapper _mapper;

        public CategoryMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryVM>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }


        public override Category MapFromDestinationToSource(CategoryVM destinationEntity)
        {
            return _mapper.Map<Category>(destinationEntity);
        }

        public override CategoryVM MapFromSourceToDestination(Category sourceEntity)
        {
            return _mapper.Map<CategoryVM>(sourceEntity);
        }

    }
}
