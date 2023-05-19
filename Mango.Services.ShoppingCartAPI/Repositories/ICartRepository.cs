using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI.Repositories
{
    public interface ICartRepository
    {
        Task<bool> ClearCart(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cart);
        Task<CartDto> GetCartByUserId(string userId);
        Task<bool> RemoveFromCart(int cartDetailsId);
    }
}
