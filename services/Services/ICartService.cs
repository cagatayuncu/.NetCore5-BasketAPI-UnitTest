using System.Threading.Tasks;
using ShoppingCart.Domain.Messages;
using ShoppingCart.Domain.Model.Dtos;

namespace ShoppingCart.Domain.Services.Services
{
    public interface ICartService
    {
        Task<ObjectResponse<BasketDto>> AddToCartAsync(BasketDto basketDto);
    }
}
