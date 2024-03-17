using Microsoft.AspNetCore.Mvc;

namespace RecipeBook.Api.Controllers
{
    public class RecipeController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
