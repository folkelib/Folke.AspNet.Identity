using Folke.AspNet.Identity;
using Folke.Orm;
using Folke.Orm.Mysql;
using Microsoft.Owin;
using Owin;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(MvcApp.Startup))]
namespace MvcApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var session = new FolkeConnection(new MySqlDriver(), ConfigurationManager.ConnectionStrings["FolkeIdentity"].ConnectionString);
            session.UpdateSchema(typeof(RoleUser).Assembly);
        }
    }
}
