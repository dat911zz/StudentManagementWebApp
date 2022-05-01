using Castle.Windsor;
using StudentManagementWebApp.Installer;
using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Utilites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Controllers
{
    public class StudentController : Controller
    {
        //static IList<Student> studentList = new List<Student>{
        //        new Student("1","Test", "nam", new DateTime(1999,1,21), "T1", "CNTT"),
        //        new Student("2","Test", "nam", new DateTime(1989,1,15), "T1", "CNTT"),
        //        new Student("3","Test", "nam", new DateTime(1789,1,4), "T1", "CNTT"),
        //        new Student("4","Test", "nam", new DateTime(1679,1,1), "T1", "CNTT"),
        //        new Student("5","Test", "nam", new DateTime(1789,8,6), "T1", "CNTT"),
        //        new Student("6","Test", "nam", new DateTime(1678,7,1), "T1", "CNTT"),
        //        new Student("7","Test", "nam", new DateTime(1666,6,1), "T1", "CNTT"),
        //    };

        WindsorContainer container;
        Manager mng;
        IStudentService service_sv;
        ISubjectService service_mh;
        static List<Student> studentList = new List<Student>();
        static List<Subject> subjectList = new List<Subject>();
        public void SetupContainer()
        {
            container = new WindsorContainer();
            container.Install(new ServicesInstaller());
            mng = container.Resolve<Manager>();
            service_sv = container.Resolve<IStudentService>();
            service_mh = container.Resolve<ISubjectService>();
            container.Dispose();
        }
        // GET: Student
        public ActionResult Index()
        {
            SetupContainer();
            //fetch students from the DB using Entity Framework here
            
            studentList = service_sv.GetAll();
            subjectList = service_mh.GetAll();

            mng.AutoWork(ref studentList, subjectList);
            //dgvc.BindDataGridView(dataGridView1);
            //Thiếu phần fill 
            return View(studentList);
        }

        //// POST: Student
        //[HttpPost]
        //public ActionResult Edit(Student st)
        //{
            
        //}
    }
}