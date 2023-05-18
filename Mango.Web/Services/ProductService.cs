using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        const string PRODUCT_ROUTE_BASE = "api/products/";
        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product, string accessToken)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = product,
                RequestType = StaticDetails.ApiType.POST,
                Url = new Uri(StaticDetails.ProductApiBase + PRODUCT_ROUTE_BASE).AbsoluteUri,
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string accessToken)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = string.Empty,
                RequestType = StaticDetails.ApiType.DELETE,
                Url = new Uri(StaticDetails.ProductApiBase + PRODUCT_ROUTE_BASE + $"{id}").AbsoluteUri,
            });
        }

        public async Task<T> GetProductAsync<T>(int id, string accessToken)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = string.Empty,
                RequestType = StaticDetails.ApiType.GET,
                Url = new Uri(StaticDetails.ProductApiBase + PRODUCT_ROUTE_BASE + $"{id}").AbsoluteUri,
            });
        }

        public async Task<T> GetProductsAsync<T>(string accessToken)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = string.Empty,
                RequestType = StaticDetails.ApiType.GET,
                Url = new Uri(StaticDetails.ProductApiBase + PRODUCT_ROUTE_BASE).AbsoluteUri,
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto product,string accessToken)
        {
            return await SendAsync<T>(new ApiRequest
            {
                AccessToken = accessToken,
                Data = product,
                RequestType = StaticDetails.ApiType.PUT,
                Url = new Uri(StaticDetails.ProductApiBase + PRODUCT_ROUTE_BASE).AbsoluteUri,
            });
        }
    }
}
