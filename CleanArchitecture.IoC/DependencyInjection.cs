using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infra.Data.Context;
using CleanArchitecture.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(
               configuration.GetConnectionString("DefaultConnection"),
               Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.7-MariaDB"), 
               b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
               ));

            //services.AddDbContext<ApplicationDbContext>(
            //    options => options.UseSqlServer(
            //        configuration.GetConnectionString("DefaultConnection"), 
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            // ));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}
