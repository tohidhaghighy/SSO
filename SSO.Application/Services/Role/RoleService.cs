using Authentication_Server.Core.Contracts.Role;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Application.Services.Role
{
    public class RoleService : IRoleService
    {
        private AuthenticationDbContext _dbContext;
        public RoleService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<SSO.Core.Models.Role> DeleteRole(int id)
        {
            var result = await _dbContext.Roles.Where(o => o.Id == id)
                 .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.Roles.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<SSO.Core.Models.Role>> GetApplicationRole(int applicationId)
        {
            var result = await _dbContext.Roles.Where(a => a.ApplicationId == applicationId).ToListAsync();
            return result;
        }

        public async Task<SSO.Core.Models.Role> GetRole(int Id)
        {
            var result = await _dbContext.Roles.Where(a=>a.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<SSO.Core.Models.Role>> GetRoleList(int applicationId, string name, string userPanel)
        {
            var result = await _dbContext.Roles.Where(a=>a.ApplicationId==applicationId && a.Name.Contains(name) && a.UrlPanel.Contains(userPanel)).ToListAsync();
            return result;
        }

        public async Task<SSO.Core.Models.Role> InsertRole(SSO.Core.Models.Role role)
        {
            role.InsertDate = DateTime.Now;
            role.UpdateDate=DateTime.Now;
            role.InsertUserName = "admin";
            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<SSO.Core.Models.Role> UpdateRole(SSO.Core.Models.Role role)
        {
            var result = await _dbContext.Roles.Where(o => o.Id == role.Id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = role.Name;
                result.UrlPanel = role.UrlPanel;
                result.UpdateDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
