using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NantechCargo.az.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tarif()
        {
            return View();
        }
        public IActionResult Xeberler()
        {
            return View();
        }
        public IActionResult Elaqe()
        {
            return View();
        }
        public IActionResult Korporativ()
        {
            return View();
        }
        public IActionResult XidmetSebekesi()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
    }
}
