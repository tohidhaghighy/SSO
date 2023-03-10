using Authentication_Server.Core.Contracts.User;
using Microsoft.EntityFrameworkCore;
using SSO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication_Server.Application.Services.User
{
    public class UserService : IUserService
    {
        private AuthenticationDbContext _dbContext;
        public UserService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<SSO.Core.Models.User> DeleteUser(int id)
        {
            var result = await _dbContext.Users.Where(o => o.Id == id)
                 .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.Users.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<SSO.Core.Models.User>> GetUserList(int applicationId, string fullName, string userName, string mobile, string email, int roleId)
        {
            var result = await _dbContext.Users.Where(a => a.ApplicationId == applicationId && (a.Name.Contains(fullName) || a.Family.Contains(fullName)) && a.UserName.Contains(userName) && a.Mobile.Contains(mobile) && a.Email.Contains(email)).ToListAsync();
            if (roleId!=0)
            {
                return result.Where(a => a.RoleId == roleId).ToList();
            }
            return result;
        }

        public async Task<SSO.Core.Models.User> GetUser(string accessToken)
        {
            var result = await _dbContext.Users.Where(a => a.Token == accessToken).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SSO.Core.Models.User> GetUserById(int id)
        {
            var result = await _dbContext.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SSO.Core.Models.User> InsertUser(SSO.Core.Models.User user)
        {
            user.InsertDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            user.Token = "";
            user.RefreshToken = "";
            user.RefreshTokenExpiryTime = DateTime.Now;
            user.InsertUserName = "admin";
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<SSO.Core.Models.User> ActiveUser(SSO.Core.Models.User user)
        {
            user.InsertDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return user;
        }


        public async Task<List<SSO.Core.Models.User>> Login(int applicationId, string userName, string password)
        {
            var result = await _dbContext.Users.Where(a => a.ApplicationId == applicationId && a.UserName == userName && a.Password==password && a.Active==true).ToListAsync();
            return result;
        }

        public async Task<SSO.Core.Models.User> UpdateUser(SSO.Core.Models.User user)
        {
            var result = await _dbContext.Users.Where(o => o.Id == user.Id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = user.Name;
                result.Family=user.Family;
                result.UserName = user.UserName;
                result.Mobile = user.Mobile;
                result.Email = user.Email;
                result.UpdateDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<SSO.Core.Models.User> UpdateUserToken(int userid ,string accessToken,string refreshToken,DateTime tokenExpiredTime)
        {
            var result = await _dbContext.Users.Where(o => o.Id == userid)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Token = accessToken;
                result.RefreshToken = refreshToken;
                result.RefreshTokenExpiryTime = tokenExpiredTime;

                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
