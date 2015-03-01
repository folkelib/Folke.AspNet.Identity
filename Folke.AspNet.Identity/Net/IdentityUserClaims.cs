using Folke.Orm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_UserClaims")]
    public class IdentityUserClaim
    {
        public IdentityUserClaim()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public IdentityUser User { get; set; }
    }
}
