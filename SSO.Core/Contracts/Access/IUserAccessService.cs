using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.Access
{
    public interface IUserAccessService
    {
        Task<List<SSO.Core.Models.UserAccess>> GetUserAccessList(int userId, int accessId);
        Task<SSO.Core.Models.UserAccess> InsertUserAccess(SSO.Core.Models.UserAccess userAccess);
        Task<SSO.Core.Models.UserAccess> DeleteUserAccess(int id);
    }
}
