using SSO.Core.Common;
using SSO.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SSO.Core.Models
{
    public class Access : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string EndpointType { get; set; }

        [Required]
        public int ApplicationId { get; set; }
    }
}
