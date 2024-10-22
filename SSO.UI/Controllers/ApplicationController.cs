using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using SSO.Core.Models;

namespace Authentication_Server.UI.Controllers
{
    public class ApplicationController : Controller
    {
        public ApplicationController(IApplicationService applicationService,IUserService userService,IRoleService roleService)
        {
            ApplicationService = applicationService;
            UserService = userService;
            RoleService = roleService;
        }

        public IApplicationService ApplicationService { get; }
        public IUserService UserService { get; }
        public IRoleService RoleService { get; }

        public async Task<IActionResult> Index(string accessKey)
        {
            return View(await ApplicationService.GetApplicationList(accessKey,""));
        }

        public async Task<IActionResult> Insert()
        {
            return View(await ApplicationService.GetApplicationList("", ""));
        }

        [HttpPost]
        public async Task<JsonResult> Insert(string accessToken, string name, string urlpanel,string keypanel,string icon)
        {
            try
            {
                if (accessToken == "" || accessToken == null)
                {
                    return Json(new { status = false, message = "توکن در درخواست وجود ندارد" });
                }
                var user = await UserService.GetUser();
                if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
                {
                    return Json(new { status = false, message = "کاربر مورد نظر وجود ندارد" });
                }
                var application = await ApplicationService.InsertApplication(new SSO.Core.Models.Application()
                {
                    GeneratedKey = keypanel,
                    Icon = icon,
                    InnerLink=urlpanel,
                    InsertDate = DateTime.Now,
                    Name=name,
                    InsertUserName=user.Name
                });

                var role = await RoleService.InsertRole(new Role()
                {
                    ApplicationId = application.Id,
                    Name = "Admin",
                    UrlPanel = "https://localhost:7062/Home/Index"
                });

                var insertedUser = await UserService.InsertUser(new SSO.Core.Models.User()
                {
                    ApplicationId=application.Id,
                    Name=name,
                    Active = true,
                    Email=user.Email,
                    Image=icon,
                    UserName= "superuser",
                    Password=user.Password,
                    Family="",
                    RoleId=role.Id,
                    InsertDate=DateTime.Now,
                    InsertUserName=user.Name,
                    Mobile=user.Mobile
                });

                return Json(new { status = true, message = "ثبت با موفقیت انجام شد کمی بعد به لیست برنامه ها منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }

        }


        public async Task<IActionResult> Update(int id)
        {
            var findapplication = await ApplicationService.GetApplicationList("", "");
            return View(findapplication.Where(a=>a.Id==id).FirstOrDefault());
        }

        [HttpPost]
        public async Task<JsonResult> Update(string accessToken,int id, string name, string urlpanel, string keypanel, string icon)
        {
            try
            {
                if (accessToken == "" || accessToken == null)
                {
                    return Json(new { status = false, message = "توکن در درخواست وجود ندارد" });
                }
                var user = await UserService.GetUser();
                if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
                {
                    return Json(new { status = false, message = "کاربر مورد نظر وجود ندارد" });
                }
                var application = await ApplicationService.UpdateApplication(new SSO.Core.Models.Application()
                {
                    Id = id,
                    GeneratedKey = keypanel,
                    Icon = icon,
                    InnerLink = urlpanel,
                    InsertDate = DateTime.Now,
                    Name = name,
                    InsertUserName = user.Name
                });

                return Json(new { status = true, message = "ویرایش با موفقیت انجام شد کمی بعد به لیست برنامه ها منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }

        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
               
                var application = await ApplicationService.DeleteApplication(id);

                return Json(new { status = true, message = "حذف با موفقیت انجام شد کمی بعد به لیست برنامه ها منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }

        }


    }
}
