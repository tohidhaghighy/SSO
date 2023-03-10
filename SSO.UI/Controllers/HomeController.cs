using SSO.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Authentication_Server.Core.Contracts.User;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.UI.ViewModel;

namespace SSO.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public HomeController(ILogger<HomeController> logger,IUserService userService,IRoleService roleService)
        {
            _logger = logger;
            this.userService = userService;
            this.roleService = roleService;
        }

        public async Task<IActionResult> Index(string accessToken)
        {
            var mainModel = new MainViewModel();
            if (accessToken=="" || accessToken == null)
            {
                return Redirect("/Login");
            }
            var user = await userService.GetUser(accessToken);
            if (user == null || user.RefreshTokenExpiryTime<DateTime.Now)
            {
                return Redirect("/Login");
            }
            var userRole = await roleService.GetRoleList(user.ApplicationId, "", "");
            if (userRole.Count()>0)
            {
                var roleList = new List<RoleViewModel>();
                foreach (var item in userRole)
                {
                    var finduser= await userService.GetUserList(user.ApplicationId,"","","","",item.Id);
                    roleList.Add(new RoleViewModel()
                    {
                        RoleId = item.Id,
                        RoleName = item.Name,
                        UserCount=finduser.Count(),
                    });
                }
                mainModel.RoleList = roleList;
                var userlist =await userService.GetUserList(user.ApplicationId, "", "", "", "", 0);
                mainModel.UserList = userlist.Where(a=>a.Active==false).
                    OrderByDescending(a=>a.Id).ToList();
            }
            return View(mainModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}