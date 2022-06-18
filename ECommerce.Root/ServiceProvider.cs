using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerce.Root
{
    public static class ServiceProvider
    {
        public static IServiceCollection BuildServiceProvider(IConfiguration configuration, IServiceCollection services)
        {
            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddDbContext<ECommerceDbContext>((serviceProvider, dbContextBuilder) =>
            //{
            //    var connectionStringPlaceHolder = configuration.GetConnectionString("DefaultConnection");
            //    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //    var dbName = httpContextAccessor.HttpContext != null ? httpContextAccessor.HttpContext.Request.Headers["tenantId"].FirstOrDefault() : "ESCPortal";
            //    var connectionString = connectionStringPlaceHolder.Replace("{dbName}", dbName);
            //    dbContextBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Efinance.ESC.Service.Infrastructure"));
            //});
            var dbConnectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ECommerceDbContext>(opt => opt.UseSqlServer(dbConnectionString, b => b.MigrationsAssembly("ECommerce.Infrastructure")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<IUnitOfWork, ECommerceUnitOfWork>();

            return services;
        }
    }
}
