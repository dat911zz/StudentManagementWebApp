using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StudentManagementWebApp.Utilites;
using StudentManagementWebApp.Data.Database;
using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Services;

namespace StudentManagementWebApp.Installer
{
    public class ServicesInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Hàm thiết lập tùy chỉnh để đăng ký các Components vào DI container
        /// </summary>
        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Manager only
            container.Register(
                Component
                    .For<Manager>()
                    .LifestyleTransient());
            //Services only
            container.Register(
                Component
                    .For<ISubjectData, IStudentData, ICourseData>()
                    .ImplementedBy<SQL>()
                    .LifestyleTransient());
            container.Register(
                Component
                    .For<ISubjectData, IStudentData, ICourseData>()
                    .ImplementedBy<Data.ORM.NHibernate_>()
                    .DependsOn(Dependency.OnValue("connectionString", DatabaseHelper.GenerateConnectionString("", "SinhVien", "test01", "1234")))
                    );
            container.Register(
                Component
                    .For<ISubjectData, IStudentData, ICourseData>()
                    .ImplementedBy<Data.ORM.Dapper>()
                    .DependsOn(Dependency.OnValue("connectionString", DatabaseHelper.GenerateConnectionString("", "SinhVien", "test01", "1234")))
                    );
            
            
            container.Register(
                Component
                    .For<IStudentService>()
                    .ImplementedBy<StudentService>()
                    .LifestyleTransient());           
            container.Register(
                Component
                    .For<ISubjectService>()
                    .ImplementedBy<SubjectService>()
                    .LifestyleTransient());
            container.Register(
                Component
                    .For<ScoreService>()
                    .LifestyleTransient());
            container.Register(
                Component
                    .For<ResultService>()
                    .LifestyleTransient());

        }
    }
}
