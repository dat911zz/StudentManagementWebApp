﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StudentManagementWebApp.Utilites;
using StudentManagementWebApp.Core.Data.Database;
using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Services;

namespace StudentManagementWebApp.Container
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
                    .For<IStudentData, ISubjectData, IUsersData, IResultData>()
                    .ImplementedBy<SQL>()
                    .LifestyleTransient());
            container.Register(
                Component
                    .For<ISubjectData, IStudentData>()
                    .ImplementedBy<Core.Data.ORM.NHibernate_>()
                    .DependsOn(Dependency.OnValue("connectionString", DatabaseHelper.GenerateConnectionString("", "SinhVien", "test01", "1234")))
                    );
            container.Register(
                Component
                    .For<ISubjectData, IStudentData>()
                    .ImplementedBy<Core.Data.ORM.Dapper>()
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
                    .For<IUsersService>()
                    .ImplementedBy<UsersService>()
                    .LifestyleTransient());
            container.Register(
                Component
                    .For<ICourseService>()
                    .ImplementedBy<CourseService>()
                    .LifestyleTransient());
            container.Register(
                Component
                    .For<IResultService>()
                    .ImplementedBy<ResultService>()
                    .LifestyleTransient());
            container.Register(
                Component
                .For<ScoreService>()
                .LifestyleTransient());
        }
    }
}
