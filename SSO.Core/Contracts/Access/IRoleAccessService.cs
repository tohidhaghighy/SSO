using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.Access
{
    public interface IRoleAccessService
    {
        Task<List<SSO.Core.Models.RoleAccess>> GetRoleAccessList(int roleId,int accessId);
        Task<SSO.Core.Models.RoleAccess> InsertRoleAccess(SSO.Core.Models.RoleAccess roleAccess);
        Task<SSO.Core.Models.RoleAccess> DeleteRoleAccess(int id);
    }
}
