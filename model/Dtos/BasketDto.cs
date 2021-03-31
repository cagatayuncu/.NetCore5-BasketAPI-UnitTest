using System;

namespace ShoppingCart.Domain.Model.Dtos
{
    public class BasketDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public BasketItemDto BasketItem { get; set; }
    }
}
