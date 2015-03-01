using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Folke.Orm;

namespace Folke.AspNet.Identity
{
    public class RoleUser
    {

        public RoleUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set; }
        public IdentityRole IdentityRole { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}