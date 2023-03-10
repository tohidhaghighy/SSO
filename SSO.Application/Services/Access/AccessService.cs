using Authentication_Server.Core.Contracts.Access;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.Access
{
    public class AccessService : IAccessService
    {
        private AuthenticationDbContext _dbContext;
        public AccessService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<SSO.Core.Models.Access> DeleteAccess(int id)
        {
            var result = await _dbContext.Accesses.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (result!=null)
            {
                _dbContext.Accesses.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<SSO.Core.Models.Access> GetAccess(int id)
        {
            var result = await _dbContext.Accesses.Where(o => o.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<SSO.Core.Models.Access>> GetAccessList(int applicationId, string name, string endPoint)
        {
            var result = await _dbContext.Accesses.Where(o =>o.ApplicationId==applicationId && o.Name.Contains(name) && o.Endpoint.Contains(endPoint)).ToListAsync();
            return result;
        }

        public async Task<SSO.Core.Models.Access> InsertAccess(SSO.Core.Models.Access access)
        {
            access.InsertDate = DateTime.Now;
            access.UpdateDate= DateTime.Now;
            access.InsertUserName = "admin";
            await _dbContext.Accesses.AddAsync(access);
            await _dbContext.SaveChangesAsync();
            return access;
        }

        public async Task<SSO.Core.Models.Access> UpdateAccess(SSO.Core.Models.Access access)
        {
            var result = await _dbContext.Accesses.Where(o => o.Id == access.Id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = access.Name;
                result.Endpoint = access.Endpoint;
                result.EndpointType= access.EndpointType;
                result.UpdateDate= DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
