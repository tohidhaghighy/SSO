using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SSO.Utility;
using Utility.Hash;
using Utility.Token;

namespace SSO.UI.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(IOptions<JwtInfo> options, IApplicationService applicationService,IUserService userService, IRoleService roleService)
        {
            Options = options;
            ApplicationService = applicationService;
            UserService = userService;
            RoleService = roleService;
        }

        public IOptions<JwtInfo> Options { get; }
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
            return View();//await RoleService.GetApplicationRole(result.FirstOrDefault().Id)
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
                    var accessToken = GenerateJWTToken.GenerateToken(new UserModel()
                    {
                        Family =finduser.FirstOrDefault().Family,
                        Name= finduser.FirstOrDefault().Name,
                        Mobile = finduser.FirstOrDefault().Mobile,
                        UserId = finduser.FirstOrDefault().Id,
                        Username = finduser.FirstOrDefault().UserName,
                        UserRole=finduser.FirstOrDefault().RoleId,
                    },new JwtInfo()
                    {
                        Issuer=Options.Value.Issuer,
                        Audience=Options.Value.Audience,
                        Key=Options.Value.Key,
                    });
                    var refreshToken = TokenGenerator.GenerateToken();
                    var expiredTime = DateTime.Now.AddDays(1);
                    userInfo.Token = accessToken;
                    userInfo.RefreshToken = refreshToken;
                    userInfo.RefreshTokenExpiryTime = expiredTime;
                    var updateUser = await UserService.UpdateUserToken(userInfo.Id, accessToken, refreshToken, expiredTime);
                    if (updateUser != null)
                    {
                        return Json(new { userid=userInfo.Id ,url = findrole.UrlPanel , accessToken = System.Web.HttpUtility.UrlEncode(accessToken)  , refreshToken = System.Web.HttpUtility.UrlEncode(refreshToken) , expiredTime = System.Web.HttpUtility.UrlEncode(expiredTime.ToString()) , status = true , message = "لاگین با موفقیت انجام شد کمی بعد به سایت مربوطه منتقل می شوید"});
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
