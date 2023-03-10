using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.Application
{
    public interface IApplicationRedirectUrlService
    {
        Task<List<SSO.Core.Models.ApplicationRediretUrl>> GetApplicationRedirectUrlList(int id);
        Task<SSO.Core.Models.ApplicationRediretUrl> InsertApplicationRedirectUrl(SSO.Core.Models.ApplicationRediretUrl applicationRedirectUrl);
        Task<SSO.Core.Models.ApplicationRediretUrl> DeleteApplicationRedirectUrl(int id);
    }
}
