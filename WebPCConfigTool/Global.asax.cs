using System;
using System.Data.Entity;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using WebPCConfigTool.DAL;
using WebPCConfigTool.DAL.Migrations;

namespace WebPCConfigTool
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseModelContext, Configuration>());
            using (var context = new DatabaseModelContext())
            {
                context.Database.Initialize(force: false);
            }
        }
    }
}