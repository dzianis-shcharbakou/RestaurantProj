using Mango.Service.ProductAPI.Models.Dto;

namespace Mango.Service.ProductAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
