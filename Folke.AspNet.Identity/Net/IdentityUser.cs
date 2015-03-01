using Folke.Orm;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_Users")]
    public class IdentityUser : IUser
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
            /*Claims = new List<IdentityUserClaim>();
            Roles = new List<RoleUser>();
            Logins = new List<IdentityUserLogin>();*/
        }

        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
            Id = Guid.NewGuid().ToString(); ;
        }

        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool TwoFactorEnabled { get; set; }
        /*[FolkeList(Join = "IdentityRole")]
        public IList<RoleUser> Roles { get; set; }
        [FolkeList(Join = "Claims")]
        public IList<IdentityUserClaim> Claims { get; set; }
        [FolkeList(Join = "Logins")]
        public IList<IdentityUserLogin> Logins { get; set; }*/
    }
}
