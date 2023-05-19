using Mango.Web.Models;
using Mango.Web.Models.ShoppingCart;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        private const string PRODUCT_ROUTE_BASE = "api/cart/";

        public ShoppingCartService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string accessToken = null)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = cartDto,
                RequestType = StaticDetails.ApiType.POST,
                Url = new Uri(StaticDetails.ShoppingCartApi + PRODUCT_ROUTE_BASE).AbsoluteUri,
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string accessToken = null)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                RequestType = StaticDetails.ApiType.GET,
                Url = new Uri(StaticDetails.ShoppingCartApi + PRODUCT_ROUTE_BASE + $"{userId}").AbsoluteUri,
            });
        }

        public async Task<T> RemoveCartAsync<T>(int cartId, string accessToken = null)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = cartId,
                RequestType = StaticDetails.ApiType.DELETE,
                Url = new Uri(StaticDetails.ShoppingCartApi + PRODUCT_ROUTE_BASE).AbsoluteUri,
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string accessToken = null)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = cartDto,
                RequestType = StaticDetails.ApiType.PUT,
                Url = new Uri(StaticDetails.ShoppingCartApi + PRODUCT_ROUTE_BASE).AbsoluteUri,
            });
        }
    }
}
