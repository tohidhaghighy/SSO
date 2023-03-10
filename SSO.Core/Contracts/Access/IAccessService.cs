using Authentication_Server.Core.Common;

namespace Authentication_Server.Core.Contracts.Access
{
    public interface IAccessService
    {
        Task<List<SSO.Core.Models.Access>> GetAccessList(int applicationId ,string name, string endPoint);
        Task<SSO.Core.Models.Access> GetAccess(int id);
        Task<SSO.Core.Models.Access> InsertAccess(SSO.Core.Models.Access access);
        Task<SSO.Core.Models.Access> DeleteAccess(int id);
        Task<SSO.Core.Models.Access> UpdateAccess(SSO.Core.Models.Access access);
    }
}
