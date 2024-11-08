﻿using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Utility.Hash;
using Utility.Token;

namespace SSO.UI.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(IApplicationService applicationService,IUserService userService, IRoleService roleService)
        {
            ApplicationService = applicationService;
            UserService = userService;
            RoleService = roleService;
        }

        public IApplicationService ApplicationService { get; }
        public IUserService UserService { get; }
        public IRoleService RoleService { get; }

        public async Task<IActionResult> Index(string key)
        {
            string url = "";
            var result =await ApplicationService.GetApplicationList(key, url);
            if (result.Count()==0 || key==null || key=="")
            {
                return Redirect("/Error/DontAccess");
            }

            ViewData["Image"] = result.FirstOrDefault().Icon;
            ViewData["Name"] = result.FirstOrDefault().Name;
            ViewData["ApplicationId"] = result.FirstOrDefault().Id;
            return View(await RoleService.GetApplicationRole(result.FirstOrDefault().Id));
        }

        [HttpPost]
        public async Task<JsonResult> Login(int applicationId,string userName,string password)
        {
            try
            {
                var hashpass = HashConverter.HashPass(password);
                var finduser = await UserService.Login(applicationId, userName, hashpass);
                if (finduser.Count()>0)
                {
                    var userInfo = finduser.FirstOrDefault();
                    var findrole = await RoleService.GetRole(userInfo.RoleId);
                    var accessToken = TokenGenerator.GenerateToken();
                    var refreshToken = TokenGenerator.GenerateToken();
                    var expiredTime = DateTime.Now.AddDays(1);
                    userInfo.Token = accessToken;
                    userInfo.RefreshToken = refreshToken;
                    userInfo.RefreshTokenExpiryTime = expiredTime;
                    var updateUser = await UserService.UpdateUserToken(userInfo.Id, accessToken, refreshToken, expiredTime);
                    if (updateUser != null)
                    {
                        return Json(new { url = findrole.UrlPanel , accessToken = System.Web.HttpUtility.UrlEncode(accessToken)  , refreshToken = System.Web.HttpUtility.UrlEncode(refreshToken) , expiredTime = System.Web.HttpUtility.UrlEncode(expiredTime.ToString()) , status = true , message = "لاگین با موفقیت انجام شد کمی بعد به سایت مربوطه منتثل می شوید"});
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { data = new { applicationId = applicationId, userName = userName, password = password }, status = false, message = ex.Message });
            }
            return Json(new { data = new { applicationId = applicationId, userName = userName, password = password }, status = false, message = "نا موفق" });
        }

        public async Task<IActionResult> RecoveryPass(string key)
        {
            string url = ""/*Request.Host.ToString()*/;
            var result = await ApplicationService.GetApplicationList(key, url);
            if (result.Count() == 0 || key == null || key == "")
            {
                return Redirect("/Error/DontAccess");
            }

            return View();
        }
    }
}
