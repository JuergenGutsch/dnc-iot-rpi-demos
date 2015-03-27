using Microsoft.AspNet.Builder;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.DependencyInjection;

namespace ReadSensors
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
            });

            app.UseServices(services =>
            {
                services.AddMvc();
                services.AddSignalR(options =>
                {
                    options.Hubs.EnableDetailedErrors = true;
                });
            });
            
            app.UseSignalR();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Home", action = "Index"});
            });

            app.UseWelcomePage();
        }
    }
}
