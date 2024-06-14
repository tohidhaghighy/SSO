using SSO.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class UserAccess : BaseModel<int>
    {
        public int UserId { get; set; }
        public int AccessId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("AccessId")]
        public Access Accesses { get; set; }
    }
}
