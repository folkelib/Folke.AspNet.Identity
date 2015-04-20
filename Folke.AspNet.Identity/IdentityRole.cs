using System;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Folke.Orm;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_Roles")]
    public class IdentityRole : IdentityRole<string>, IRole
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    [Table("aspnet_Roles")]
    public class IdentityRole<TKey> : IRole<TKey>
    {
        public IdentityRole()
            : this("")
        {
        }

        public IdentityRole(string roleName)
        {
            Name = roleName;
        }

        [Key]
        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}
