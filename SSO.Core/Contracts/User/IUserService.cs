using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.User
{
    public interface IUserService
    {
        Task<List<SSO.Core.Models.User>> GetUserList(int applicationId, string fullName, string userName,string mobile,string email,int roleId);
        Task<SSO.Core.Models.User> GetUser(string accessToken);
        Task<SSO.Core.Models.User> GetUserById(int id);
        Task<SSO.Core.Models.User> ActiveUser(SSO.Core.Models.User user);
        Task<List<SSO.Core.Models.User>> Login(int applicationId, string userName, string password);
        Task<SSO.Core.Models.User> InsertUser(SSO.Core.Models.User user);
        Task<SSO.Core.Models.User> DeleteUser(int id);
        Task<SSO.Core.Models.User> UpdateUser(SSO.Core.Models.User user);
        Task<SSO.Core.Models.User> UpdateUserToken(int userid, string accessToken, string refreshToken, DateTime tokenExpiredTime);
    }
}
