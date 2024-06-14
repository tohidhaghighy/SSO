using Authentication_Server.Core.Contracts.City;
using Authentication_Server.Core.Contracts.Province;
using Authentication_Server.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_Server.UI.Controllers
{
    public class UserAddressController : Controller
    {
        public UserAddressController(ICityService cityService, IUserAddressService userAddressService, IProvinceService provinceService)
        {
            CityService = cityService;
            UserAddressService = userAddressService;
            ProvinceService = provinceService;
        }

        public ICityService CityService { get; }
        public IUserAddressService UserAddressService { get; }
        public IUserService UserService { get; }
        public IProvinceService ProvinceService { get; }

        public async Task<IActionResult> Index(int userId)
        {
            var result = await UserAddressService.GetUserAddressList(userId);
            return View(result);
        }
    }
}
