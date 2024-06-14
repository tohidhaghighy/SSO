using SSO.Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Models
{
    public class Role : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UrlPanel { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        //[Required]
        //public bool ShowInRegister { get; set; }

        public virtual List<User> Users { get; set;}
    }
}