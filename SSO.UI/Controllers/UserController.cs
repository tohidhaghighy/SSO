using Authentication_Server.Application.Services.User;
using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;
using Authentication_Server.Core.Dtos.User;
using Authentication_Server.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using SSO.Core.Models;
using Utility.Hash;

namespace SSO.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IApplicationService applicationService;
        private readonly IAccessService accessService;
        private readonly IUserAccessService userAccessService;
        private readonly IRoleAccessService roleAccessService;

        public UserController(ILogger<HomeController> logger, IUserService userService, IRoleService roleService,IApplicationService applicationService,IAccessService accessService,IUserAccessService userAccessService,IRoleAccessService roleAccessService)
        {
            _logger = logger;
            this.userService = userService;
            this.roleService = roleService;
            this.applicationService = applicationService;
            this.accessService = accessService;
            this.userAccessService = userAccessService;
            this.roleAccessService = roleAccessService;
        }

        public async Task<IActionResult> Index(string accessToken,int role)
        {
            if (accessToken == "" || accessToken == null)
            {
                return Redirect("/Login");
            }
            var user = await userService.GetUser();
            
            var userList = await userService.GetUserList(2, "", "", "", "", 0);
            var listDto = new List<UserDto>();
            foreach (var item in userList)
            {
                var roleInfo= await roleService.GetRole(item.RoleId);
                listDto.Add(new UserDto()
                {
                    RoleId = item.RoleId,
                    ApplicationId = item.ApplicationId,
                    RoleName=roleInfo.Name,
                    Active=item.Active,
                    Email=item.Email,
                    FullName=item.Name+' '+item.Family,
                    Id=item.Id,
                    Image=item.Image,
                    InsertDate=item.InsertDate,
                    InsertUserName=item.InsertUserName,
                    Mobile=item.Mobile,
                    Password=item.Password,
                    UserName=item.UserName,
                });
            }

            ViewData["Name"] = "";
            return View(listDto);
        }

        public async Task<PartialViewResult> Search(string accessToken, int role,string name,string mobile,string email)
        {
            if (name == null) name = "";
            if (mobile == null) mobile = "";
            if (email == null) email = "";

            var listDto = new List<UserDto>();
            if (accessToken == "" || accessToken == null)
            {
                return PartialView("_userList", listDto);
            }
            var user = await userService.GetUser();
            var userList = await userService.GetUserList(2, name, "", mobile, email , role);
            foreach (var item in userList)
            {
                var roleInfo = await roleService.GetRole(item.RoleId);
                listDto.Add(new UserDto()
                {
                    RoleId = item.RoleId,
                    ApplicationId = item.ApplicationId,
                    RoleName = roleInfo.Name,
                    Active = item.Active,
                    Email = item.Email,
                    FullName = item.Name+' '+item.Family,
                    Id = item.Id,
                    Image = item.Image,
                    InsertDate = item.InsertDate,
                    InsertUserName = item.InsertUserName,
                    Mobile = item.Mobile,
                    Password = item.Password,
                    UserName = item.UserName,
                });
            }

            return PartialView("_userList",listDto);
        }

        public async Task<IActionResult> Insert(string accessToken)
        {
            if (accessToken == "" || accessToken == null)
            {
                return Redirect("/Login");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(string accessToken,string name,string family,string email,string mobile,string username,string password,int roleid,IFormFile files)
        {
            try
            {
                if (accessToken == "" || accessToken == null)
                {
                    return Redirect("/Login");
                }
                var user = await userService.GetUser();
                string imagePath = "";
                if (files != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image");

                    //create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    //get file extension
                    FileInfo fileInfo = new FileInfo(files.FileName);
                    string fileName = Guid.NewGuid() + fileInfo.Extension;
                    imagePath = "/image/" + fileName;
                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                }
                var hashpass = HashConverter.HashPass(password);
                var insertedUser = await userService.InsertUser(new Core.Models.User()
                {
                    UserName = username,
                    Password = hashpass,
                    Active = true,
                    ApplicationId = 2,
                    Email = (email==null?"": email),
                    Mobile = mobile,
                    Name = name,
                    Family = family,
                    RoleId = roleid,
                    Image = imagePath,
                });
                return Json(new { status = true , message = "ثبت با موفقیت انجام شد کمی بعد به لیست کاربران منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertBeforeLogin(string key, string name, string family, string email, string mobile, string username, string password, int roleid, IFormFile files)
        {
            try
            {
                var result = await applicationService.GetApplicationList(key, "");
                if (result.Count() == 0 || key == null || key == "")
                {
                    return Redirect("/Error/DontAccess");
                }
                string imagePath = "";
                var hashpass = HashConverter.HashPass(password);
                var insertedUser = await userService.InsertUser(new Core.Models.User()
                {
                    UserName = username,
                    Password = hashpass,
                    Active = false,
                    ApplicationId = result.FirstOrDefault().Id,
                    Email = (email == null ? "" : email),
                    Mobile = mobile,
                    Name = name,
                    Family = family,
                    RoleId = roleid,
                    Image = "",
                });
                return Json(new { status = true, message = "ثبت با موفقیت انجام شد منتظر تایید مدیر برای ورود به سایت باشید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

        public async Task<IActionResult> Update(string accessToken,int id)
        {
            if (accessToken == "" || accessToken == null)
            {
                return Redirect("/Login");
            }
            var user = await userService.GetUser();
            if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return Redirect("/Login");
            }
            return View(await userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(string accessToken,int id, string name, string family, string email, string mobile, string username, string password, int roleid, IFormFile files)
        {
            try
            {
                if (accessToken == "" || accessToken == null)
                {
                    return Redirect("/Login");
                }
                var user = await userService.GetUserById(id);
                if (user == null)
                {
                    return Redirect("/Login");
                }
                string imagePath = "";
                if (files != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image");

                    //create folder if not exist
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    //get file extension
                    FileInfo fileInfo = new FileInfo(files.FileName);
                    string fileName = Guid.NewGuid() + fileInfo.Extension;
                    imagePath = "/image/" + fileName;
                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                }
                else
                {
                    imagePath = user.Image; 
                }
                var hashpass = "";
                if (password.Trim()!="")
                {
                    hashpass = HashConverter.HashPass(password);
                }

                var insertedUser = await userService.UpdateUser(new Core.Models.User()
                {
                    Id=id,
                    UserName = username,
                    Password = hashpass,
                    ApplicationId = user.ApplicationId,
                    Email = (email == null ? "" : email),
                    Mobile = mobile,
                    Name = name,
                    Family = family,
                    RoleId = roleid,
                    Image = imagePath,
                });
                return Json(new { status = true, message = "ویرایش با موفقیت انجام شد کمی بعد به لیست کاربران منتقل می شوید" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }


        public async Task<IActionResult> InsesrtUserAccess(string accessToken, int id)
        {
            if (accessToken == "" || accessToken == null)
            {
                return Redirect("/Login");
            }
            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return Redirect("/Login");
            }
            var unSelectedAccessList = new List<Access>();
            var accessList = await roleAccessService.GetRoleAccessList(user.RoleId,0);
            foreach (var item in accessList)
            {
                var currentaccess = await userAccessService.GetUserAccessList(id, item.Id);
                if (currentaccess.Count == 0)
                {
                    var acceessinfo = await accessService.GetAccess(item.AccessId);
                    unSelectedAccessList.Add(acceessinfo);
                }
            }
            var roleAccess = await userAccessService.GetUserAccessList(id, 0);
            var selectedAccessList = new List<Access>();
            foreach (var item in roleAccess)
            {
                var access = await accessService.GetAccess(item.AccessId);
                selectedAccessList.Add(access);
            }
            var roleAccessViewModel = new RoleAcceessViewModel();
            roleAccessViewModel.SelectedAccess = selectedAccessList;
            roleAccessViewModel.unSelectedAccess = unSelectedAccessList;
            return View(roleAccessViewModel);
        }

        /// <summary>
        /// if reqType== 1 insert
        /// if reqType== 2 delete
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userid"></param>
        /// <param name="reqType"></param>
        /// <returns></returns>
        public async Task<PartialViewResult> UserAccessList(string accessToken, int userId, int reqType)
        {
            if (accessToken == "" || accessToken == null)
            {
                return PartialView("_accessInsertList", null);
            }
            var user = await userService.GetUserById(userId);
            if (user == null)
            {
                return PartialView("_accessInsertList", null);
            }
            var unSelectedAccessList = new List<Access>();
            var accessList = await accessService.GetAccessList(user.ApplicationId, "", "");
            foreach (var item in accessList)
            {
                var currentaccess = await userAccessService.GetUserAccessList(userId, item.Id);
                if (currentaccess.Count == 0)
                {
                    unSelectedAccessList.Add(item);
                }
            }
            var roleAccess = await userAccessService.GetUserAccessList(userId, 0);
            var selectedAccessList = new List<Access>();
            foreach (var item in roleAccess)
            {
                var access = await accessService.GetAccess(item.AccessId);
                selectedAccessList.Add(new Access()
                {
                    ApplicationId = user.ApplicationId,
                    Id = item.Id,
                    InsertDate = item.InsertDate,
                    Name = access.Name,
                    Endpoint = access.Endpoint,
                });
            }

            if (reqType == 1)
            {
                return PartialView("_accessInsertList", unSelectedAccessList);
            }
            else
            {
                return PartialView("_accessDeleteList", selectedAccessList);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Active(int id)
        {
            try
            {
                var user = await userService.GetUserById(id);
                user.Active = true;
                await userService.UpdateUser(user);
                return Json(new { status = true, message = "با موفقیت فعال شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> DeActive(int id)
        {
            try
            {
                var user = await userService.GetUserById(id);
                user.Active = false;
                await userService.UpdateUser(user);
                return Json(new { status = true, message = "با موفقیت فعال شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await userService.DeleteUser(id);
                return Json(new { status = true, message = "با موفقیت حذف شد" });

            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });

            }
        }
    }
}
