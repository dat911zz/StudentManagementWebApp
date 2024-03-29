﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.AspNet.SignalR;
using NLog;
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
            //RouteTable.Routes.MapHubs();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //NLog.Common.InternalLogger.LogLevel = NLog.LogLevel.Trace;
            //NLog.Common.InternalLogger.LogToConsole = true;
            //NLog.Common.InternalLogger.LogFile = @"${basedir}\internal-log.txt"; // On Linux one can use "/home/nlog-internal.txt"
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Info("Program started");
            // Calling Global action filter
            //GlobalFilters.Filters.Add(new AppExceptionHandler());
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
                else
                {
                    FormsAuthentication.SignOut();
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
            Logger logger = NLog.LogManager.GetCurrentClassLogger();
            var ex = Server.GetLastError();
            var httpException = ex as HttpException;

            if (httpException == null || httpException.GetHttpCode() != 404)
            {
                // Not an HttpException, or HTTP error other than 404.
                // Here: log error, send alert, etc.
                logger.Fatal(ex, "Aplication Error!");
                new HttpContextWrapper(Context).Response.Redirect("~/Error/PageNotFound");
            }

        }
    }
}
