using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Data;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Services.Mapper;
using ShoppingCart.Domain.Services.Services;

namespace ShoppingCart.UnitTest
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductDummyService, ProductDummyService>();
            services.AddDbContext<CartDbContext>(options => options.UseSqlite("Data Source = CartDataBase.db"));
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
