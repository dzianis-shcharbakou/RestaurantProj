using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    public class CartController : ControllerBase
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
        public async Task<object> Get(string userId)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserId(userId);
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
        public async Task<object> Post([FromBody] CartDto cartDto)
        {
            try
            {
                var cartDtoResult = await _cartRepository.CreateUpdateCart(cartDto);
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
        public async Task<object> Put([FromBody] CartDto cartDto)
        {
            try
            {
                var cartDtoResult = await _cartRepository.CreateUpdateCart(cartDto);
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
        public async Task<object> Delete([FromBody] int cartId)
        {
            try
            {
                var isSuccess = await _cartRepository.RemoveFromCart(cartId);
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
