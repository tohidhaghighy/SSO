using Authentication_Server.Core.Contracts.Application;
using Microsoft.AspNetCore.Mvc;

namespace SSO.UI.Controllers
{
    public class RegisterController : Controller
    {
        public RegisterController(IApplicationService applicationService)
        {
            ApplicationService = applicationService;
        }

        public IApplicationService ApplicationService { get; }

        public async Task<IActionResult> Index(string key)
        {
            string url = "";
            var result = await ApplicationService.GetApplicationList(key, url);
            if (result.Count() == 0 || key == null || key == "")
            {
                return Redirect("/Error/DontAccess");
            }

            ViewData["Image"] = result.FirstOrDefault().Icon;
            ViewData["Name"] = result.FirstOrDefault().Name;
            ViewData["ApplicationId"] = result.FirstOrDefault().Id;
            return View();
        }
    }
}
