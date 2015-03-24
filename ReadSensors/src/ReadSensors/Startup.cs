using Microsoft.AspNet.Builder;
using Microsoft.AspNet.SignalR;
using Microsoft.Framework.DependencyInjection;
using ReadSensors.Infrastructure;


namespace HelloMvc
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();

            app.UseStaticFiles();

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
