using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Authentication_Server.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using SSO.Core.Models;

namespace SSO.UI.Controllers
{
    public class RoleController : Controller
    {
        public RoleController(IRoleService roleService, IUserService userService,IAccessService accessService,IRoleAccessService roleAccessService,IUserAccessService userAccessService)
        {
            RoleService = roleService;
            UserService = userService;
            AccessService = accessService;
            RoleAccessService = roleAccessService;
            UserAccessService = userAccessService;
        }

        public IRoleService RoleService { get; }
        public IUserService UserService { get; }
        public IAccessService AccessService { get; }
        public IRoleAccessService RoleAccessService { get; }
        public IUserAccessService UserAccessService { get; }

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
            return View(await RoleService.GetRoleList(user.ApplicationId,"",""));
        }

        public async Task<PartialViewResult> Search(string accessToken,string name="",string urlpanel="")
        {
            if (name==null) name = "";
            if (urlpanel == null) urlpanel = "";
            
            if (accessToken == "" || accessToken == null)
            {
                return PartialView("_roleList", null);
            }
            var user = await UserService.GetUser(accessToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return PartialView("_roleList", null);
            }
            return PartialView("_roleList",await RoleService.GetRoleList(user.ApplicationId,name,urlpanel));
        }

        public async Task<IActionResult> Insert(string accessToken)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Insert(string accessToken, string name, string urlpanel)
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
                await RoleService.InsertRole(new Core.Models.Role()
                {
                    ApplicationId = user.ApplicationId,
                    Name = name,
                    UrlPanel= urlpanel
                });
                return Json(new { status = true, message = "ثبت با موفقیت انجام شد کمی بعد به لیست نقش ها منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }

        }

        public async Task<IActionResult> InsertRoleAccess(string accessToken,int roleid)
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
            var unSelectedAccessList = new List<Access>();
            var accessList = await AccessService.GetAccessList(user.ApplicationId, "", "");
            foreach (var item in accessList)
            {
                var currentaccess = await RoleAccessService.GetRoleAccessList(roleid, item.Id);
                if (currentaccess.Count==0)
                {
                    unSelectedAccessList.Add(item);
                }
            }
            var roleAccess = await RoleAccessService.GetRoleAccessList(roleid, 0);
            var selectedAccessList = new List<Access>();
            foreach (var item in roleAccess)
            {
                var access = await AccessService.GetAccess(item.AccessId);
                selectedAccessList.Add(new Access()
                {
                    ApplicationId = user.ApplicationId,
                    Id= item.Id,
                    InsertDate = item.InsertDate,
                    Name=access.Name,
                    Endpoint=access.Endpoint,
                });
            }
            var roleAccessViewModel = new RoleAcceessViewModel();
            roleAccessViewModel.SelectedAccess = selectedAccessList;
            roleAccessViewModel.unSelectedAccess = unSelectedAccessList;
            return View(roleAccessViewModel);
        }

        /// <summary>
        /// if reqType== 1 insert
        /// if reqType== 2 delete
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="roleid"></param>
        /// <param name="reqType"></param>
        /// <returns></returns>
        public async Task<PartialViewResult> RoleAccessList(string accessToken, int roleid, int reqType)
        {
            if (accessToken == "" || accessToken == null)
            {
                return PartialView("_accessInsertList", null);
            }
            var user = await UserService.GetUser(accessToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return PartialView("_accessInsertList", null);
            }
            var unSelectedAccessList = new List<Access>();
            var accessList = await AccessService.GetAccessList(user.ApplicationId, "", "");
            foreach (var item in accessList)
            {
                var currentaccess = await RoleAccessService.GetRoleAccessList(roleid, item.Id);
                if (currentaccess.Count == 0)
                {
                    unSelectedAccessList.Add(item);
                }
            }
            var roleAccess = await RoleAccessService.GetRoleAccessList(roleid, 0);
            var selectedAccessList = new List<Access>();
            foreach (var item in roleAccess)
            {
                var access = await AccessService.GetAccess(item.AccessId);
                selectedAccessList.Add(new Access()
                {
                    ApplicationId = user.ApplicationId,
                    Id = item.Id,
                    InsertDate = item.InsertDate,
                    Name = access.Name,
                    Endpoint = access.Endpoint,
                });
            }

            if (reqType==1)
            {
                return PartialView("_accessInsertList", unSelectedAccessList);
            }
            else
            {
                return PartialView("_accessDeleteList", selectedAccessList);
            }
            
        }

        public async Task<IActionResult> Update(string accessToken,int id)
        {
            return View(await RoleService.GetRole(id));
        }

        [HttpPost]
        public async Task<JsonResult> Update(string accessToken,int id, string name, string urlpanel)
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
                await RoleService.UpdateRole(new Core.Models.Role()
                {
                    Id = id,
                    ApplicationId = user.ApplicationId,
                    Name = name,
                    UrlPanel = urlpanel
                });
                return Json(new { status = true, message = "ویرایش با موفقیت انجام شد کمی بعد به لیست نقش ها منتقل می شوید" });

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
                var user = await RoleService.DeleteRole(id);
                return Json(new { status = true, message = "با موفقیت حذف شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

    }
}
