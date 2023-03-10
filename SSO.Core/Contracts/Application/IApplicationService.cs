using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.Application
{
    public interface IApplicationService
    {
        Task<List<SSO.Core.Models.Application>> GetApplicationList(string key,string url);
        Task<SSO.Core.Models.Application> InsertApplication(SSO.Core.Models.Application application);
        Task<SSO.Core.Models.Application> DeleteApplication(int id);
        Task<SSO.Core.Models.Application> UpdateApplication(SSO.Core.Models.Application application);
    }
}
