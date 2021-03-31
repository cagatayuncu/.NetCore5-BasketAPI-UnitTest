using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Domain.Messages;
using ShoppingCart.Domain.Model.Dtos;
using ShoppingCart.Domain.Model.Entities;

namespace ShoppingCart.Domain.Services.Services
{
    public class ProductDummyService : IProductDummyService
    {
        public ProductDummyService()
        {
            
        }
        public async Task<Product> GetProductById(Guid id)
        {
            var list = new List<Product>()
            {
                new Product
                {

                    Category = "Monitor",
                    Description = "Test",
                    Id = Guid.Parse("291338bf-c07d-4ff4-81e1-bee8d5ee8070"),
                    Price = 55,
                    Quantity = 5,
                    Title = "Asus"
                },
                new Product
                {

                    Category = "Laptop",
                    Description = "Test",
                    Id = Guid.Parse("abb63822-7c6a-45e5-b6a8-6e049a281c08"),
                    Price = 60,
                    Quantity = 5,
                    Title = "Apple"
                }, 
                new Product
                {

                    Category = "Keyboard",
                    Description = "Test",
                    Id = Guid.Parse("049939f1-141a-454f-939a-9868315e114d"),
                    Price =40,
                    Quantity = 5,
                    Title = "Acer"
                },
                new Product
                {

                    Category = "Mouse",
                    Description = "Test",
                    Id = Guid.Parse("54691d7e-a715-4851-9cfb-757b8230877f"),
                    Price = 100,
                    Quantity = 0,
                    Title = "Monster"
                },
                new Product
                {

                    Category = "Monitor",
                    Description = "Test",
                    Id = Guid.Parse("405dcc80-cfe6-4eb5-93ed-2e91f9761881"),
                    Price = 200,
                    Quantity = 54,
                    Title = "Dell"
                },
            };
            var response = list.FirstOrDefault(p => p.Id == id);
            return response;
        }
    }
}
