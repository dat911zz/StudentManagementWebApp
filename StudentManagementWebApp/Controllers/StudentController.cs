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
        WindsorContainer container;
        Manager mng;
        IStudentService service_sv;
        ISubjectService service_mh;
        static List<Student> studentList = new List<Student>();
        static List<Subject> subjectList = new List<Subject>();

        [NonAction]
        public void SetupContainer()
        {
            container = new WindsorContainer();
            container.Install(new ServicesInstaller());
            mng = container.Resolve<Manager>();
            service_sv = container.Resolve<IStudentService>();
            service_mh = container.Resolve<ISubjectService>();
            container.Dispose();
        }

        public ActionResult LoadDB()
        {
            SetupContainer();
            studentList = service_sv.GetAll();
            subjectList = service_mh.GetAll();
            mng.AutoWork(ref studentList, subjectList);
            return RedirectToAction("Index");
        }

        // GET: Student
        public ActionResult Index()
        {
            SetupContainer();
            //fetch students from the DB using Entity Framework here
                      
            
            return View(studentList.OrderBy(s=>s.Id));
        }

        //// GET: Student
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            return View(std);
        }
        /// <summary>
        /// POST: Student
        /// </summary>
        /// <param name="std"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Student std)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            var student = studentList.Where(s => s.Id == std.Id).FirstOrDefault();
            studentList.Remove(student);
            studentList.Add(std);

            return RedirectToAction("Index");
        }
    }
}