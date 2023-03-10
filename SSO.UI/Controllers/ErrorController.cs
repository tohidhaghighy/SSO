using Microsoft.AspNetCore.Mvc;

namespace SSO.UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }

        public IActionResult Error503()
        {
            return View();
        }

        public IActionResult DontAccess()
        {
            return View();
        }
    }
}
