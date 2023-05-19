using Mango.Web.Models.ShoppingCart;

namespace Mango.Web.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveCartAsync<T>(int cartId, string token = null);
    }
}
