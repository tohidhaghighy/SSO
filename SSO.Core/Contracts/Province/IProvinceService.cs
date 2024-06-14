using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.Province
{
    public interface IProvinceService
    {
        Task<List<SSO.Core.Models.Province>> GetProvinceList();
    }
}
