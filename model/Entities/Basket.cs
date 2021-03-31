
using System;

namespace ShoppingCart.Domain.Model.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public BasketItem BasketItem { get; set; }
    }
}
