using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ResponseDto _responseDto;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                var cartDto = _cartRepository.GetCartByUserId(userId);
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPost]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                var cartDtoResult = _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cartDtoResult;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPut]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                var cartDtoResult = _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cartDtoResult;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpDelete]
        public async Task<object> RemoveCart([FromBody]int cartId)
        {
            try
            {
                var isSuccess = _cartRepository.RemoveFromCart(cartId);
                _responseDto.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _responseDto;
        }
    }
}
