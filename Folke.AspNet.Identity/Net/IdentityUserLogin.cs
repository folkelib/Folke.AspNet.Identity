using Folke.Orm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_UserLogins")]
    public class IdentityUserLogin
    {
        public IdentityUserLogin()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public IdentityUser User { get; set; }
    }
}
