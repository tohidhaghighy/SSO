using Authentication_Server.Core.Contracts.Province;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.Province
{
    public class ProvinceService : IProvinceService
    {
        private AuthenticationDbContext _dbContext;
        public ProvinceService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<List<SSO.Core.Models.Province>> GetProvinceList()
        {
            var result = await _dbContext.Provinces.ToListAsync();
            return result;
        }
    }
}
