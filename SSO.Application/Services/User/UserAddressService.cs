using Authentication_Server.Core.Contracts.User;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Models;
using SSO.Infrastructure;

namespace Authentication_Server.Application.Services.User
{
    public class UserAddressService : IUserAddressService
    {
        private AuthenticationDbContext _dbContext;
        public UserAddressService(AuthenticationDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public async Task<UserAddress> DeleteUserAddress(int id)
        {
            var result = await _dbContext.UserAddresses.Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                _dbContext.UserAddresses.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<UserAddress>> GetUserAddressList(int userId)
        {
            var result = await _dbContext.UserAddresses.Where(a => a.UserId==userId).ToListAsync();
            return result;
        }

        public async Task<UserAddress> InsertUserAddress(UserAddress userAddress)
        {
            userAddress.InsertDate = DateTime.Now;
            await _dbContext.UserAddresses.AddAsync(userAddress);
            await _dbContext.SaveChangesAsync();
            return userAddress;
        }

        public async Task<UserAddress> UpdateUserAddress(UserAddress userAddress)
        {
            var result = await _dbContext.UserAddresses.Where(o => o.Id == userAddress.Id)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = userAddress.Name;
                result.GpsX = userAddress.GpsX;
                result.GpsY = userAddress.GpsY;
                result.Address = userAddress.Address;
                result.UpdateDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
