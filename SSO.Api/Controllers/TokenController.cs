using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SSO.Api.Enums;
using SSO.Api.Infrastructure.Common;
using SSO.Api.Infrastructure.Model;
using System.Linq.Expressions;

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

        [HttpPost("Token")]
        public async Task<JsonResult> Post([FromBody]userToken userToken)
        {
            var findUser = await userService.GetUserById(userToken.userid);
            var ListRoles = await roleService.GetRoleList(2, "", "");
            if (findUser == null)
                return Json(new { success = false, error = "توکن مورد نظر موجود نیست" });
            if (findUser.Id== userToken.userid)
            {
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        UserId = userToken.userid,
                        Name = findUser.Name,
                        Family = findUser.Family,
                        Username = findUser.UserName,
                        Mobile = findUser.Mobile,
                        UserRole = findUser.RoleId.ToString(),
                        UserRoleList = ListRoles.Select(x=>new
                        {
                            x.Id,
                            x.Name
                        })
                    },
                    error = ""
                });
            }
            return Json(new { success = false, error = "اطلاعات کاربر و توکن تطابق ندارد" });
        }

        [HttpGet("GetUserRole")]
        public async Task<JsonResult> GetUserRole(int roleId)
        {
            var userListInfo = new List<userInfo>();
            var userList = new List<Core.Models.User>();

            var findrole=await roleService.GetRole(roleId);

            switch (findrole.VirtualId)
            {
                case (int)RoleType.adminsta:
                    findrole = await roleService.GetRoleByVirtualRole((int)RoleType.usersta);
                    userList = await userService.GetRelatedUsers(findrole.Id);
                    break;
                case (int)RoleType.adminina:
                    findrole = await roleService.GetRoleByVirtualRole((int)RoleType.userina);
                    userList = await userService.GetRelatedUsers(findrole.Id);
                    break;
                case (int)RoleType.adminita:
                    findrole = await roleService.GetRoleByVirtualRole((int)RoleType.userita);
                    userList = await userService.GetRelatedUsers(findrole.Id);
                    break;
                case (int)RoleType.admindir:
                    findrole = await roleService.GetRoleByVirtualRole((int)RoleType.admindir);
                    userList = await userService.GetRelatedUsers(findrole.Id);
                    break;
                case (int)RoleType.ticketingAdmin:
                    findrole = await roleService.GetRoleByVirtualRole((int)RoleType.ticketingAdmin);
                    userList = await userService.GetRelatedUsers(findrole.Id);
                    break;
            }

            foreach (var item in userList)
            {
                userListInfo.Add(new userInfo()
                {
                    UserId = item.Id,
                    Name = item.Name +" "+ item.Family,
                    RoleId = item.RoleId,
                    VirtualId = item.Role.VirtualId ?? 0
                });
            }

            return Json(new { success = true, data = userListInfo });
        }
    }

    public class userToken
    {
        public int userid { get; set; }
        public string accesstoken { get; set; }
    }

    public class userInfo
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int VirtualId { get; set; }
    }
}
