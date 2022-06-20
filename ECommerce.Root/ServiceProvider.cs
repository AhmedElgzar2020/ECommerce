using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ECommerce.Application.Services;
using ECommerce.Application.Mappers;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Root
{
    public static class ServiceProvider
    {
        public static IServiceCollection BuildServiceProvider(IConfiguration configuration, IServiceCollection services)
        {
            
            var dbConnectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ECommerceDbContext>(opt => opt.UseSqlServer(dbConnectionString, b => b.MigrationsAssembly("ECommerce.Infrastructure")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ECommerceDbContext>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<IUnitOfWork, ECommerceUnitOfWork>();
            //services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<CategoryMapper>();
            services.AddScoped<ProductMapper>();

            return services;
        }
    }
}
