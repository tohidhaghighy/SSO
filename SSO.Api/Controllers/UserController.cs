using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace SSO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IApplicationService applicationService;
        private readonly IAccessService accessService;
        private readonly IUserAccessService userAccessService;
        private readonly IRoleAccessService roleAccessService;

        public UserController(ILogger<UserController> logger, IUserService userService, IRoleService roleService, IApplicationService applicationService, IAccessService accessService, IUserAccessService userAccessService, IRoleAccessService roleAccessService)
        {
            _logger = logger;
            this.userService = userService;
            this.roleService = roleService;
            this.applicationService = applicationService;
            this.accessService = accessService;
            this.userAccessService = userAccessService;
            this.roleAccessService = roleAccessService;
        }

        [HttpGet("List")]
        public async Task<JsonResult> Get(string token)
        {
            var findApplication = await applicationService.GetApplicationList(token, "");
            if (findApplication == null)
                return Json(new { success=false, error = "توکن مورد نظر موجود نیست" });
            var finduserList = await userService.GetUserList(findApplication.FirstOrDefault().Id, "", "", "", "",0);
            return Json(new { success=true, data = finduserList.Select(a=>new {a.UserName,a.Id,a.Name,a.Family }),error =""});
        }
    }
}
