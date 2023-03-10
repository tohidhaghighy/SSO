using Authentication_Server.Core.Contracts.Access;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Models;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.Access
{
    public class RoleAccessService : IRoleAccessService
    {
        private AuthenticationDbContext _dbContext;
        public RoleAccessService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<RoleAccess> DeleteRoleAccess(int id)
        {
            var result = await _dbContext.RoleAccesses.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.RoleAccesses.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<RoleAccess>> GetRoleAccessList(int roleId, int accessId)
        {
            var result = await _dbContext.RoleAccesses.OrderByDescending(a=>a.Id).ToListAsync();
            if (roleId==0)
            {
                return result.Where(o => o.AccessId == accessId).ToList();
            }
            else if (accessId==0)
            {
                return result.Where(o => o.RoleId == roleId).ToList();
            }
            else
            {
                return result.Where(o => o.RoleId == roleId && o.AccessId==accessId).ToList();
            }
        }

        public async Task<RoleAccess> InsertRoleAccess(RoleAccess roleAccess)
        {
            roleAccess.InsertDate = DateTime.Now;
            await _dbContext.RoleAccesses.AddAsync(roleAccess);
            await _dbContext.SaveChangesAsync();
            return roleAccess;
        }
    }
}
