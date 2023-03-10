using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.Role
{
    public interface IRoleService
    {
        Task<List<SSO.Core.Models.Role>> GetRoleList(int applicationId,string name,string userPanel);
        Task<List<SSO.Core.Models.Role>> GetApplicationRole(int applicationId);
        Task<SSO.Core.Models.Role> GetRole(int Id);
        Task<SSO.Core.Models.Role> InsertRole(SSO.Core.Models.Role role);
        Task<SSO.Core.Models.Role> DeleteRole(int id);
        Task<SSO.Core.Models.Role> UpdateRole(SSO.Core.Models.Role role);
    }
}
