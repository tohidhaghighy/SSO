using Authentication_Server.Core.Contracts.Application;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.Application
{
    public class ApplicationService : IApplicationService
    {
        private AuthenticationDbContext _dbContext;
        public ApplicationService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<SSO.Core.Models.Application> DeleteApplication(int id)
        {
            var result = await _dbContext.Applications.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.Applications.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<SSO.Core.Models.Application>> GetApplicationList(string key,string url)
        {
            var result = await _dbContext.Applications.Where(a=>a.GeneratedKey == key && a.InnerLink.Contains(url)).ToListAsync();
            return result;
        }

        public async Task<SSO.Core.Models.Application> InsertApplication(SSO.Core.Models.Application application)
        {
            application.InsertDate = DateTime.Now;
            await _dbContext.Applications.AddAsync(application);
            await _dbContext.SaveChangesAsync();
            return application;
        }

        public async Task<SSO.Core.Models.Application> UpdateApplication(SSO.Core.Models.Application application)
        {
            var result = await _dbContext.Applications.Where(o => o.Id == application.Id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = application.Name;
                result.InnerLink = application.InnerLink;
                result.UpdateDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
