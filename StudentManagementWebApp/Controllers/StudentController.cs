using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Utilites;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using StudentManagementWebApp.Container;
using System.Web.Security;

namespace StudentManagementWebApp.Controllers
{
    [HandleError]
    [Authorize]
    public class StudentController : Controller
    {
        
        Manager mng;
        
        IStudentService service_sv;
        ISubjectService service_mh;
        static List<Student> studentList = new List<Student>();
        static List<Subject> subjectList = new List<Subject>();
        public StudentController(Manager manager, IStudentService studentService, ISubjectService subjectService)
        {
            mng = manager;
            service_sv = studentService;
            service_mh = subjectService;
        }
        public ActionResult LoadDB()
        {
            try
            {
                studentList = service_sv.GetAll();
                subjectList = service_mh.GetAll();
                mng.AutoWork(ref studentList, subjectList);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }
            
        }
        #region Index
        // GET: Student
        [AuthorizeRole(Role.USER, Role.ADMIN)]
        public ActionResult Index()
        {
            ViewBag.TotalStudents = studentList.Count;
            //fetch students from the DB using Entity Framework here

            return View(studentList.OrderBy(s => int.Parse(s.Id)));
        }
        #endregion
        #region Edit
        /// <summary>
        /// GET: Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeRole(Role.ADMIN)]
        public ActionResult Edit(int id)
        {
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            if (std == null)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }
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
            service_sv.Add(std);

            //update list by removing old student and adding updated student for demo purpose
            var student = studentList.Where(s => s.Id == std.Id).FirstOrDefault();
            studentList.Remove(student);
            studentList.Add(std);



            return RedirectToAction("Index");
        }
        #endregion
        #region Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            if(std == null)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }

            ViewBag.ResultList = Manager.ConvertDataTableToHTML(mng.UploadSubjectSVIntoDataTable(std));           
            return View(std);
        }
        [HttpGet]
        public ActionResult CourseDetails(int id)
        {
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            if (std == null)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }
            ViewBag.Id = id;
            ViewBag.CourseList = Manager.ConvertDataTableToHTML(mng.UploadSubjectSVIntoDataTable(std));

            return View("CourseDetails");
        }
        [HttpGet]
        public ActionResult ResultDetails(int id)
        {
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            if (std == null)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }
            ViewBag.Id = id;
            ViewBag.ResultList = Manager.ConvertDataTableToHTML(mng.UploadScoreSVIntoDataTable(std));
            return View("ResultDetails");
        }
        #endregion
        #region Create
        [HttpGet]
        [AuthorizeRole(Role.ADMIN)]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Student std)
        {
            int lastId = 1;
            if (studentList.Count != 0)
            {
                lastId = int.Parse(studentList.Last().Id);              
            }
            std.Id = "" + ++lastId;
            studentList.Add(std);
            return RedirectToAction("Index");
        }
        #endregion
        #region Delete
        [HttpGet]
        [AuthorizeRole(Role.ADMIN)]
        public ActionResult Delete(int id)
        {
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            studentList.Remove(std);
            return RedirectToAction("Index");
        }
        #endregion
    }
}