using SSO.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class User : BaseModel<int>
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
