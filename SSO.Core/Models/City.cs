using SSO.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class City : BaseModel<int>
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
    }
}
