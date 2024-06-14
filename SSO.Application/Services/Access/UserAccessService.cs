using Authentication_Server.Core.Contracts.Access;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Models;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.Access
{
    public class UserAccessService : IUserAccessService
    {
        private AuthenticationDbContext _dbContext;
        public UserAccessService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<UserAccess> DeleteUserAccess(int id)
        {
            var result = await _dbContext.UserAccesses.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.UserAccesses.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<UserAccess>> GetUserAccessList(int userId, int accessId)
        {
            var result = await _dbContext.UserAccesses.ToListAsync();
            if (userId == 0)
            {
                return result.Where(o => o.AccessId == accessId).ToList();
            }
            else if (accessId == 0)
            {
                return result.Where(o => o.UserId == userId).ToList();
            }
            else
            {
                return result.Where(o => o.UserId == userId && o.AccessId == accessId).ToList();
            }
        }

        public async Task<UserAccess> InsertUserAccess(UserAccess userAccess)
        {
            userAccess.InsertDate = DateTime.Now;
            await _dbContext.UserAccesses.AddAsync(userAccess);
            await _dbContext.SaveChangesAsync();
            return userAccess;
        }
    }
}
