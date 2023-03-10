using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace SSO.UI.Controllers
{
    public class AccessController : Controller
    {
        public AccessController(IAccessService accessService,IUserService userService,IUserAccessService userAccessService,IRoleAccessService roleAccessService)
        {
            AccessService = accessService;
            UserService = userService;
            UserAccessService = userAccessService;
            RoleAccessService = roleAccessService;
        }

        public IAccessService AccessService { get; }
        public IUserService UserService { get; }
        public IUserAccessService UserAccessService { get; }
        public IRoleAccessService RoleAccessService { get; }

        public async Task<IActionResult> Index(string accessToken)
        {
            if (accessToken == "" || accessToken == null)
            {
                return Redirect("/Login");
            }
            var user = await UserService.GetUser(accessToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return Redirect("/Login");
            }
            return View(await AccessService.GetAccessList(user.ApplicationId,"",""));
        }

        public async Task<PartialViewResult> Search(string accessToken,string name,string endpoint)
        {
            if (name == null) name = "";
            if (endpoint == null) endpoint = "";
            if (accessToken == "" || accessToken == null)
            {
                return PartialView("_AccessList", null);
            }
            var user = await UserService.GetUser(accessToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return PartialView("_AccessList", null);
            }
            return PartialView("_AccessList",await AccessService.GetAccessList(user.ApplicationId, name, endpoint));
        }

        public async Task<IActionResult> Insert(string accessToken)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Insert(string accessToken, string name, string endpoint, string requesttype)
        {
            try
            {
                if (accessToken == "" || accessToken == null)
                {
                    return Json(new { status = false, message = "توکن در درخواست وجود ندارد" });
                }
                var user = await UserService.GetUser(accessToken);
                if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
                {
                    return Json(new { status = false, message = "کاربر مورد نظر وجود ندارد" });
                }
                await AccessService.InsertAccess(new Core.Models.Access()
                {
                    ApplicationId = user.ApplicationId,
                    Endpoint = endpoint,
                    Name=name,
                    EndpointType=requesttype,
                });
                return Json(new { status = true, message = "ثبت با موفقیت انجام شد کمی بعد به لیست دسترسی ها منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
            
        }

        public async Task<IActionResult> Update(string accessToken,int id)
        {
            return View(await AccessService.GetAccess(id));
        }

        [HttpPost]
        public async Task<JsonResult> Update(string accessToken,int id, string name, string endpoint, string requesttype)
        {
            try
            {
                if (accessToken == "" || accessToken == null)
                {
                    return Json(new { status = false, message = "توکن در درخواست وجود ندارد" });
                }
                var user = await UserService.GetUser(accessToken);
                if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
                {
                    return Json(new { status = false, message = "کاربر مورد نظر وجود ندارد" });
                }
                await AccessService.UpdateAccess(new Core.Models.Access()
                {
                    Id = id,
                    ApplicationId = user.ApplicationId,
                    Endpoint = endpoint,
                    Name = name,
                    EndpointType = requesttype,
                });
                return Json(new { status = true, message = "ویرایش با موفقیت انجام شد کمی بعد به لیست دسترسی ها منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await AccessService.DeleteAccess(id);
                var useracceslist = await UserAccessService.GetUserAccessList(0, id);
                foreach (var item in useracceslist)
                {
                    await UserAccessService.DeleteUserAccess(item.Id);
                }
                var roleaccesslist = await RoleAccessService.GetRoleAccessList(0, id);
                foreach (var item in roleaccesslist)
                {
                    await RoleAccessService.DeleteRoleAccess(item.Id);
                }
                return Json(new { status = true, message = "با موفقیت حذف شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

    }
}
