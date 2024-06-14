using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SSO.Api.Infrastructure.Common;
using SSO.Api.Infrastructure.Model;

namespace SSO.Api.Controllers
{
    public class TokenController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IApplicationService applicationService;
        private readonly IAccessService accessService;
        private readonly IUserAccessService userAccessService;
        private readonly IRoleAccessService roleAccessService;
        public IOptions<JwtInfo> Options { get; }

        public TokenController(IOptions<JwtInfo> options , ILogger<UserController> logger, IUserService userService, IRoleService roleService, IApplicationService applicationService, IAccessService accessService, IUserAccessService userAccessService, IRoleAccessService roleAccessService)
        {
            Options = options;
            _logger = logger;
            this.userService = userService;
            this.roleService = roleService;
            this.applicationService = applicationService;
            this.accessService = accessService;
            this.userAccessService = userAccessService;
            this.roleAccessService = roleAccessService;
        }

        [HttpGet("Token/{userid}")]
        public async Task<JsonResult> Get(int userid , string accesstoken)
        {
            var findUser = await userService.GetUser(accesstoken);
            if (findUser == null)
                return Json(new { success = false, error = "توکن مورد نظر موجود نیست" });
            if (findUser.Id==userid)
            {
                return Json(new { success = true, token = GenerateJWTToken.GenerateToken(new Infrastructure.Model.UserModel()
                {
                    UserId = userid,
                    Name=findUser.Name,
                    Family=findUser.Family,
                    Username=findUser.UserName,
                    Mobile=findUser.Mobile,
                    UserRole=findUser.RoleId.ToString()
                },Options.Value.Key,Options.Value.Audience,Options.Value.Issuer), error = "" });
            }
            return Json(new { success = false, error = "اطلاعات کاربر و توکن تطابق ندارد" });
        }
    }
}
