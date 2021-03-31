using AutoMapper;
using ShoppingCart.Domain.Model.Dtos;
using ShoppingCart.Domain.Model.Entities;

namespace ShoppingCart.Domain.Services.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketDto, Basket>();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
