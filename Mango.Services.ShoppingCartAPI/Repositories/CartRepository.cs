using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _applicationContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                var cartDetailsToRemove = _applicationContext.CartDetails.Where(x => x.CartHeaderId == cartHeaderFromDb.Id);
                _applicationContext.CartDetails.RemoveRange(cartDetailsToRemove);
                _applicationContext.CartHeaders.Remove(cartHeaderFromDb);
                await _applicationContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);

            var prodInDb = await _applicationContext.Products.FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId);
            if (prodInDb == null)
            {
                _applicationContext.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _applicationContext.SaveChangesAsync();
            }

            var cartHeaderFromDb = await _applicationContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                _applicationContext.CartHeaders.Add(cart.CartHeader);
                await _applicationContext.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _applicationContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _applicationContext.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await _applicationContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.Id);

                if (cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _applicationContext.Add(cart.CartDetails.FirstOrDefault());
                    await _applicationContext.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _applicationContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _applicationContext.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            var cart = new Cart();
            cart.CartHeader = await _applicationContext.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
            if (cart.CartHeader != null)
            {
                cart.CartDetails = await _applicationContext.CartDetails.Where(x => x.CartHeaderId == cart.CartHeader.Id).Include(x => x.Product).ToListAsync();
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                var cardDetails = await _applicationContext.CartDetails.FirstOrDefaultAsync(x => x.Id == cartDetailsId);
                var totalCountOfCartItems = _applicationContext.CartDetails.Where(x => x.CartHeaderId == cardDetails.CartHeaderId).Count();

                _applicationContext.CartDetails.Remove(cardDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _applicationContext.CartHeaders.FirstOrDefaultAsync(x => x.Id == cardDetails.CartHeaderId);

                    _applicationContext.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _applicationContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
