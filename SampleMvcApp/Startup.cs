using Folke.AspNet.Identity;
using Folke.Orm;
using Folke.Orm.Mapping;
using Folke.Orm.Mysql;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(MvcApp.Startup))]
namespace MvcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var session = new FolkeConnection(new MySqlDriver(), new Mapper(), ConfigurationManager.ConnectionStrings["FolkeIdentity"].ConnectionString);
            session.UpdateStringIdentityUserSchema<ApplicationUser>();
            session.UpdateStringIdentityRoleSchema();
        }
    }
}
