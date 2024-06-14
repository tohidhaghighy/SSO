using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Core.Contracts.City
{
    public interface ICityService
    {
        Task<List<SSO.Core.Models.City>> GetCityList(int provinceId);
    }
}
