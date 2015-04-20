using Folke.Orm;
using System;
using System.ComponentModel.DataAnnotations;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_UserClaims")]
    public class IdentityUserClaim<TUser, TKey> where TUser: IdentityUser<TKey>
    {
        [Key]
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public TUser User { get; set; }
    }
}
