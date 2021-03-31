using System;

namespace ShoppingCart.Domain.Model.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
        public double Price { get; set; }
    }
}
