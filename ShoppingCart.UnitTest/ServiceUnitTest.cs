using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShoppingCart.Data;
using ShoppingCart.Domain.Messages;
using ShoppingCart.Domain.Model.Dtos;
using ShoppingCart.Domain.Services.Services;
using Xunit;

namespace ShoppingCart.UnitTest
{
    public class ServiceUnitTest
    {
        private readonly ICartService _service;
        private readonly IProductDummyService _dummyService;
        public ServiceUnitTest(ICartService service, IProductDummyService dummyService)
        {
            _service = service;
            _dummyService = dummyService;
        }
       

        [Fact]
        public async void Missing_Product_Test()
        {
            var notFoundProduct = new BasketDto()
            {
                BasketItem = new BasketItemDto
                {
                    ProductId = Guid.NewGuid(),
                }
            };
            var response = await _service.AddToCartAsync(notFoundProduct);
         
            // Assert
            Assert.Equal(response.Message, "Ürün Bulunamadı");
        }
        [Fact]
        public async void Stock_Out_Products_Test()
        {
            var outOfStock = new BasketDto()
            {
                BasketItem = new BasketItemDto
                {
                    ProductId = Guid.Parse("54691d7e-a715-4851-9cfb-757b8230877f"),
                }
            };
            var response = await _service.AddToCartAsync(outOfStock);

            // Assert
            Assert.Equal(response.Message, "Ürün stokta kalmamıştır.");
        }
        [Fact]
        public async void Add_Products_To_Cart_Test()
        {
            var addToCart = new BasketDto()
            {
                UserId = new Guid(),
                BasketItem = new BasketItemDto
                {
                    ProductId = Guid.Parse("049939f1-141a-454f-939a-9868315e114d"),
                    Quantity = 2,
                    Id = new Guid()
                    
                }
                
            };
            var response = await _service.AddToCartAsync(addToCart);

            // Assert
            Assert.Equal(response.HttpResultCode, HttpResultCode.Ok);
        }
        [Fact]
        public async void Update_Products_Quantity_Test()
        {
            var addToCart = new BasketDto()
            {
                UserId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                BasketItem = new BasketItemDto
                {
                    ProductId = Guid.Parse("049939f1-141a-454f-939a-9868315e114d"),
                    Quantity = 2,
                    Id = Guid.NewGuid()

                }

            };
            var response = await _service.AddToCartAsync(addToCart);

            // Assert
            Assert.Equal(response.Message, "Updated Cart");
        }

        [Fact]
        public async void Apply_Discount_Test()
        {
            var addToCart = new BasketDto()
            {
                UserId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                BasketItem = new BasketItemDto
                {
                    ProductId = Guid.Parse("abb63822-7c6a-45e5-b6a8-6e049a281c08"),
                    Quantity = 2,
                    Id = Guid.NewGuid()

                }

            };
            var product = await _dummyService.GetProductById(addToCart.BasketItem.ProductId);

            var response = await _service.AddToCartAsync(addToCart);
            // Assert
            Assert.True(response.Item.BasketItem.Price < product.Price, "Expected response Price to be greater than Product Price.");

        }

    }
}
