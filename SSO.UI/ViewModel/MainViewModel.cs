using SSO.Core.Models;

namespace Authentication_Server.UI.ViewModel
{
    public class MainViewModel
    {
        public List<RoleViewModel> RoleList { get; set; }
        public List<User> UserList { get; set; }
    }
}
