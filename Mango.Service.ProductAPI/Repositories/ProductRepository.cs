using AutoMapper;
using Mango.Service.ProductAPI.DbContexts;
using Mango.Service.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Mango.Service.ProductAPI.Models;

namespace Mango.Service.ProductAPI.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            if (product.ProductId > 0)
            {
                _dbContext.Products.Update(product);
            }
            else
            {
                _dbContext.Products.Add(product);
            }

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ProductDto>(productDto);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                _dbContext.Products.Remove(product);
				await _dbContext.SaveChangesAsync();

				return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _dbContext.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
