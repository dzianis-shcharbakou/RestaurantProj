using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        public ShoppingCartService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            
        }
    }
}
