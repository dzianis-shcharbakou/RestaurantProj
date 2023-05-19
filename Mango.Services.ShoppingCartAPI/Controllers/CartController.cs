using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
