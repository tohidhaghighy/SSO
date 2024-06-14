namespace Authentication_Server.Core.Contracts.User
{
    public interface IUserAddressService
    {
        Task<List<SSO.Core.Models.UserAddress>> GetUserAddressList(int userId);
        Task<SSO.Core.Models.UserAddress> InsertUserAddress(SSO.Core.Models.UserAddress userAddress);
        Task<SSO.Core.Models.UserAddress> DeleteUserAddress(int id);
        Task<SSO.Core.Models.UserAddress> UpdateUserAddress(SSO.Core.Models.UserAddress userAddress);
    }
}
