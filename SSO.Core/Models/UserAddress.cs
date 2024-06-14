using SSO.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class UserAddress : BaseModel<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string GpsX { get; set; }
        public string GpsY { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
