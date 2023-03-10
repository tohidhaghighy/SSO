using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Server.UI.Controllers
{
    public class RoleAccessController : Controller
    {
        public RoleAccessController(IRoleService roleService, IUserService userService, IAccessService accessService, IRoleAccessService roleAccessService, IUserAccessService userAccessService)
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

        [HttpPost]
        public async Task<JsonResult> Insert(int roleId,int accessId)
        {
            try
            {
                await RoleAccessService.InsertRoleAccess(new SSO.Core.Models.RoleAccess()
                {
                    AccessId = accessId,
                    RoleId = roleId,
                    InsertDate=DateTime.Now,
                    InsertUserName="admin"
                });
                return Json(new { status = true, message = "با موفقیت ثبت شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteItem(int id)
        {
            try
            {
                await RoleAccessService.DeleteRoleAccess(id);
                return Json(new { status = true, message = "با موفقیت حذف شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }

        }

    }
}
