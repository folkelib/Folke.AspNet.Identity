using Folke.Orm;
using System;
using System.ComponentModel.DataAnnotations;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_UserLogins")]
    public class IdentityUserLogin<TUser, TKey> where TUser : IdentityUser<TKey>
    {
        [Key]
        public int Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public TUser User { get; set; }
    }
}
