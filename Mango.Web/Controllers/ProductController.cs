using Mango.Web.Models;
using Mango.Web.Models.Product;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = new List<ProductDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductsAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess) 
            {
                result = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(result);
        }

		public async Task<IActionResult> Create()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProductAsync<ResponseDto>(productDto, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(productDto);
        }

		public async Task<IActionResult> Edit(int productId)
		{
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductAsync<ResponseDto>(productId, accessToken);
			if (response != null && response.IsSuccess)
			{
				var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
				return View(model);
			}

			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProductAsync<ResponseDto>(productDto, accessToken);
				if (response != null && response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}

			return View(productDto);
		}

		public async Task<IActionResult> Delete(int productId)
		{
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductAsync<ResponseDto>(productId, accessToken);
			if (response != null && response.IsSuccess)
			{
				var model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
				return View(model);
			}

			return NotFound();
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.DeleteProductAsync<ResponseDto>(productDto.ProductId, accessToken);
				if (response.IsSuccess)
				{
					return RedirectToAction(nameof(Index));
				}
			}

			return View(productDto);
		}
	}
}
