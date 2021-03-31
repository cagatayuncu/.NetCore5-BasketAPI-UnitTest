using System;

namespace ShoppingCart.Domain.Model.Entities
{
    public class BasketItem
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
        public double Price { get; set; }

    }
}