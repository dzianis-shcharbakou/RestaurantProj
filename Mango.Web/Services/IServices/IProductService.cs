using Mango.Web.Models.Product;

namespace Mango.Web.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetProductsAsync<T>(string accessToken);
        Task<T> GetProductAsync<T>(int id, string accessToken);
        Task<T> CreateProductAsync<T>(ProductDto product, string accessToken);
        Task<T> UpdateProductAsync<T>(ProductDto product, string accessToken);
        Task<T> DeleteProductAsync<T>(int id, string accessToken);
    }
}
