using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using StudentManagementWebApp.Filter;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace StudentManagementWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Calling Global action filter
            GlobalFilters.Filters.Add(new AppExceptionHandler());

            new WindsorContainer().Install(FromAssembly.InDirectory(new AssemblyFilter(HttpRuntime.BinDirectory)));
        }
        protected void Application_PostAuthenticateRequest()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
        protected void Application_EndRequest()
        {
            var context = new HttpContextWrapper(Context);
            switch (context.Response.StatusCode)
            {
                case 401:
                    context.Response.Redirect("~/Error/Unauthorized");
                    break;
                case 404:
                    context.Response.Redirect("~/Error/PageNotFound");
                    break;
                default:
                    break;
            }
        }
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            var httpException = ex as HttpException;

            if (httpException == null || httpException.GetHttpCode() != 404)
            {
                // Not an HttpException, or HTTP error other than 404.
                // Here: log error, send alert, etc.
                new HttpContextWrapper(Context).Response.Redirect("~/Error/PageNotFound");
            }

        }
    }
}
