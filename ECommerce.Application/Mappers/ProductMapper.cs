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
    public class ProductMapper : MapperBase<Product, ProductVM>
    {
        private readonly IMapper _mapper;

        public ProductMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductVM>().ReverseMap();
            });
            _mapper = config.CreateMapper();
        }


        public override Product MapFromDestinationToSource(ProductVM destinationEntity)
        {
            return _mapper.Map<Product>(destinationEntity);
        }

        public override ProductVM MapFromSourceToDestination(Product sourceEntity)
        {
            return _mapper.Map<ProductVM>(sourceEntity);
        }
    }
}
