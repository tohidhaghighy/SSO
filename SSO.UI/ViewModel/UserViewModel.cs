using SSO.Core.Models;

namespace Authentication_Server.UI.ViewModel
{
    public class UserViewModel
    {
        public User User { get; set; }
        public string RoleName { get; set; }
        public List<Access> Accesses { get; set; }
    }
}
