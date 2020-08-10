using Microsoft.AspNetCore.Mvc;

namespace ProvaSolucaoInformatica.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}