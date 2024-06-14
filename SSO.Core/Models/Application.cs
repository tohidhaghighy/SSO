using SSO.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace SSO.Core.Models
{
    public class Application : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string InnerLink { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string GeneratedKey { get; set; }
    }
}
