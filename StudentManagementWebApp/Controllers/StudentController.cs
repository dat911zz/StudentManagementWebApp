using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Utilites;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using StudentManagementWebApp.Container;
using System.Web.Security;
using System;

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
        List<Subject> tmpSubjectList = new List<Subject>(); 
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

                studentList.Sort(new Comparison<Student>((x, y) => {
                    return int.Parse(x.Id) - int.Parse(y.Id);             
                }));
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }
            
        }
        #region Index
        // GET: Student
        [AuthorizeRole(Roles = "ADMIN, MODERATOR, USER")]
        public ActionResult Index()
        {
            ViewBag.TotalStudents = studentList.Count;
            //fetch students from the DB using Entity Framework here

            //return View(studentList.OrderBy(s => int.Parse(s.Id)));
            return View(studentList);
        }
        #endregion
        #region Edit
        /// <summary>
        /// GET: Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeRole(Roles = "ADMIN")]
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
            if (ModelState.IsValid)
            {
                //update student in DB using EntityFramework in real-life application
                service_sv.Update(std);

                //update list by removing old student and adding updated student for demo purpose
                var student = studentList.Where(s => s.Id == std.Id).FirstOrDefault();
                studentList.Remove(student);
                studentList.Add(std);
                
                return RedirectToAction("Index");
            }
            return View();
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
        [AuthorizeRole(Roles = "ADMIN")]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Student std)
        {
            if (ModelState.IsValidField("ClassId") && ModelState.IsValidField("CourseId") && ModelState.IsValidField("Name") && ModelState.IsValidField("Gender"))//Check đk form đã nhập chưa
            {
                int lastId = 1;
                if (studentList.Count != 0)
                {
                    lastId = int.Parse(studentList.Last().Id);              
                }
                std.Id = "" + ++lastId;
                service_sv.Add(std);
                studentList.Add(std);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion
        #region Delete
        [HttpPost]
        [AuthorizeRole(Roles = "ADMIN")]
        public ActionResult Delete(int id)
        {
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            studentList.Remove(std);
            service_sv.Remove(std.Id);
            return RedirectToAction("Index");
        }
        #endregion


        #region Course Register
        [HttpGet]
        public ActionResult DKHP()
        {
            ViewBag.Subjects = subjectList;
            ViewBag.TmpList = tmpSubjectList;
            return View("CourseRegister");
        }
        [HttpPost]
        public ActionResult AddToTmpList(Subject x)
        {
            tmpSubjectList.Add(x);
            ViewBag.TmpList = tmpSubjectList;
            return View("CourseRegister");
        }
        #endregion
    }
}