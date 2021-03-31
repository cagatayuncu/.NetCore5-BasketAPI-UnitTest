using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Messages;
using ShoppingCart.Domain.Model.Dtos;
using ShoppingCart.Domain.Model.Entities;
using ShoppingCart.Domain.Model.Enum;

namespace ShoppingCart.Domain.Services.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Basket, Guid> _repository;
        private readonly IProductDummyService _productDummyService;
        private const bool AutoSave = true;
        private const bool DiscountBoolean = true;


        public CartService(IMapper mapper, IRepository<Basket, Guid> repository, IProductDummyService productDummyService)
        {
            _mapper = mapper;
            _repository = repository;
            _productDummyService = productDummyService;
        }

        public async Task<ObjectResponse<BasketDto>> AddToCartAsync(BasketDto basketDto)
        {
            var response = new ObjectResponse<BasketDto>();

            try
            {
                var product = await _productDummyService.GetProductById(basketDto.BasketItem.ProductId);
                if (product == null)
                {
                    response.Item = basketDto;
                    response.SetFailure("Ürün Bulunamadı");
                    return response;
                }

                if (product.Quantity == 0)
                {
                    response.SetFailure("Ürün stokta kalmamıştır.");
                    return response;
                }
                var basket = await _repository.GetAsync(p => p.BasketItem.ProductId == basketDto.BasketItem.ProductId, true);

                if (basket != null && basket.BasketItem.Quantity > 0)
                {
                    basket.BasketItem.Price = product.Category == "Laptop" ? (product.Price - (product.Price * (double)Discount.BlackFriday) / 100) : product.Price;
                    basket.BasketItem.Quantity += basketDto.BasketItem.Quantity;

                    Basket updatedCart = await _repository.UpdateAsync(basket, AutoSave);
                    var updatedCartDto = _mapper.Map<Basket, BasketDto>(updatedCart);
                    response.SetSuccess(updatedCartDto);
                    response.Message = "Updated Cart";
                    return response;
                }

                var entity = _mapper.Map<BasketDto, Basket>(basketDto);
                entity.BasketItem.Price = product.Category == "Laptop" ? (product.Price - (product.Price * (double)Discount.BlackFriday) / 100) : product.Price;
                var createdCart = await _repository.AddAsync(entity, true);
                var createdCartDto = _mapper.Map<Basket, BasketDto>(createdCart);
                response.SetSuccess(createdCartDto);

                return response;
            }
            catch (Exception e)
            {
                response.SetFailure(e.Message);
                throw;
            }
        }

    }
}
