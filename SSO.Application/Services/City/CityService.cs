using Authentication_Server.Core.Contracts.City;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.City
{
    public class CityService : ICityService
    {
        private AuthenticationDbContext _dbContext;
        public CityService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<List<SSO.Core.Models.City>> GetCityList(int provinceId)
        {
            var result = await _dbContext.Cities.Where(a=>a.ProvinceId==provinceId).ToListAsync();
            return result;
        }
    }
}
