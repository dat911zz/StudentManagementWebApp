using WebActivatorEx;

[assembly: System.Web.PreApplicationStartMethod(typeof(StudentManagementWebApp.App_Start.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(StudentManagementWebApp.App_Start.WindsorActivator), "Shutdown")]


namespace StudentManagementWebApp.App_Start
{

    public class WindsorActivator  
    {  
        static ContainerBootstrapper bootstrapper;  
  
        public static void PreStart()  
        {  
            bootstrapper = ContainerBootstrapper.Bootstrap();  
        }  
  
        public static void Shutdown()  
        {  
            if (bootstrapper != null)  
                bootstrapper.Dispose();  
        }  
    }  
}