using Authentication_Server.Core.Contracts.Application;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Models;
using SSO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Application.Services.Application
{
    public class ApplicationRedirectUrlService : IApplicationRedirectUrlService
    {
        private AuthenticationDbContext _dbContext;
        public ApplicationRedirectUrlService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<ApplicationRediretUrl> DeleteApplicationRedirectUrl(int id)
        {
            var result = await _dbContext.ApplicationRediretUrls.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.ApplicationRediretUrls.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<ApplicationRediretUrl>> GetApplicationRedirectUrlList(int id)
        {
            var result = await _dbContext.ApplicationRediretUrls.Where(a=>a.ApplicationId==id).ToListAsync();
            return result;
        }

        public async Task<ApplicationRediretUrl> InsertApplicationRedirectUrl(ApplicationRediretUrl applicationRedirectUrl)
        {
            applicationRedirectUrl.InsertDate = DateTime.Now;
            await _dbContext.ApplicationRediretUrls.AddAsync(applicationRedirectUrl);
            await _dbContext.SaveChangesAsync();
            return applicationRedirectUrl;
        }
    }
}
