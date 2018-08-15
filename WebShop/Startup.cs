using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using WebShop.Core;

[assembly: OwinStartupAttribute(typeof(WebShop.Startup))]
namespace WebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new DataModule(app));

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //Register With OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}
