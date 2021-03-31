using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Domain.Model.Entities;

namespace ShoppingCart.Domain.Services.Services
{
    public interface IProductDummyService
    {
        Task<Product> GetProductById(Guid id);
    }
}
