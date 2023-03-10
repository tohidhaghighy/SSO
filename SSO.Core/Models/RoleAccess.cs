using SSO.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class RoleAccess : BaseModel<int>
    {
        public int RoleId { get; set; }
        public int AccessId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("AccessId")]
        public Access Accesses { get; set; }
    }
}
