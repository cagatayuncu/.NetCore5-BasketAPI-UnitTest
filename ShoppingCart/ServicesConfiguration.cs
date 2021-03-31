using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Services.Services;

namespace ShoppingCart
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductDummyService, ProductDummyService>();
            return services;
        }
    }

}
