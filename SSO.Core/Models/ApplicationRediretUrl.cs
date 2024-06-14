using SSO.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class ApplicationRediretUrl : BaseModel<int>
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public int ApplicationId { get; set; }

    }
}
