using Microsoft.AspNetCore.Mvc;

namespace WA.Pizza.API.Controllers
{
    public class CatalogItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
