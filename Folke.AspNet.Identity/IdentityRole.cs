using System;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Folke.Orm;

namespace Folke.AspNet.Identity
{
    [Table("aspnet_Roles")]
    public class IdentityRole : IRole
    {
        public IdentityRole()
            : this("")
        {
        }

        public IdentityRole(string roleName)
        {
            Id = Guid.NewGuid().ToString();
            Name = roleName;
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
