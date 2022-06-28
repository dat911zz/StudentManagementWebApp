using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.IO;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(StudentManagementWebApp.Startup))]

namespace StudentManagementWebApp
{
    /// <summary>
    /// Initialize 
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
