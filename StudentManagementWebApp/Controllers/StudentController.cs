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
using StudentManagementWebApp.Services;
using StudentManagementWebApp.Utilites.Comparer;
using NLog;

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
        public List<Subject> subjectQueue = new List<Subject>();

        public Logger logger = LogManager.GetCurrentClassLogger();

        public StudentController(Manager manager, IStudentService studentService, ISubjectService subjectService)
        {
            mng = manager;
            service_sv = studentService;
            service_mh = subjectService;
        }
        #region Index
        // GET: Student
        [AuthorizeRole(Roles = "ADMIN, MODERATOR, USER")]
        public ActionResult Index()
        {           
            /*LogManager.Shutdown(); */ // Remember to flush
            //fetch students from the DB using Entity Framework here
            try
            {
                studentList = service_sv.GetAll();              
                mng.AutoWork(ref studentList);

                studentList.Sort(new Comparison<Student>((x, y) => {
                    return int.Parse(x.Id.Substring(2)) - int.Parse(y.Id.Substring(2));
                }));
                ViewBag.TotalStudents = studentList.Count;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error was sent from [StudentController - Index]");
                return RedirectToAction("Expt","Error", new { mess = ex.Message.ToString() });
            }
            logger.Info("Status: Loading student list completed");
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
        public ActionResult Edit(string id)
        {
            logger.Info($"Edit {id}");
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
        public ActionResult Details(string id)
        {
            logger.Info($"Details {id}");
            var std = studentList.Where(s => s.Id.Equals(id.ToString())).FirstOrDefault();
            if(std == null)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }

            ViewBag.ResultList = Manager.ConvertDataTableToHTML(mng.UploadSubjectSVIntoDataTable(std));           
            return View(std);
        }
        [HttpGet]
        public ActionResult CourseDetails(string id)
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
        public ActionResult ResultDetails(string id)
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
            logger.Info("Create");
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
                    lastId = int.Parse(studentList.Last().Id.Substring(2));              
                }
                if (studentList.Count < 10)
                {
                    std.Id = "SV00" + ++lastId;
                }
                else
                {
                    if (studentList.Count < 100)
                    {
                        std.Id = "SV0" + ++lastId;
                    }
                    else
                    {
                        if (studentList.Count < 1000)
                        {
                            std.Id = "SV" + ++lastId;
                        }
                        else
                        {
                            std.Id = "SV" + ++lastId;
                        }
                    }

                }
                
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
        public ActionResult Delete(string id)
        {
            logger.Info($"Delete {id}");
            var std = studentList.Where(s => s.Id.Equals(id)).FirstOrDefault();
            studentList.Remove(std);
            service_sv.Remove(std.Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Course Register
        
        /// <summary>
        /// Điều hướng đến trang tìm MSSV
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreDKHP()
        {
            studentList = service_sv.GetAll();
            mng.AutoWork(ref studentList);
            return View("SearchStudentToCR");
        }
        /// <summary>
        /// Check mssv và chuyển qua cổng ĐKHP 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PreDKHP(string id)
        {
            var std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (std == null)
            {
                if (id.Equals(""))
                {
                    ViewBag.Error = "Vui lòng nhập mã sinh viên!";
                }
                else
                {
                    ViewBag.Error = $"Không tìm thấy sinh viên có mã ";
                    ViewBag.Id = id;
                }
                return View("SearchStudentToCR");
            }

            return RedirectToAction("DKHP", new { id = std.Id });//Redierect thi phải new cái id nếu không sẽ bị lộn qua controller :v
        }
        /// <summary>
        /// Controller của cổng ĐKHP
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DKHP(string id)
        {

            var std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            subjectList = service_mh.GetAll();
            //Lấy ra danh sách môn học mà sinh viên đã đăng ký
            var tmpSList = new CourseService().GetSubjectList(std.CourseDetail.ResultList);

            
            //Lấy ra danh sách môn học chưa đăng ký
            ViewBag.Subjects = subjectList.Except(tmpSList, new SubjectEComparer());
            ViewBag.TmpList = subjectQueue;
            return View("CourseRegister", std);
        }
        [HttpPost]
        public ActionResult InQueue(string id, string subId)
        {
            Student std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            subjectQueue.Add(new Subject(subjectList.Where(x => x.SubjectId.Equals(subId)).FirstOrDefault()));
            List<Subject> tmpSList = new CourseService().GetSubjectList(std.CourseDetail.ResultList);
            //ViewBag.Subjects = subjectList.Except(tmpSList, new SubjectEComparer());
            
            return RedirectToAction("DKHP", new { id = id});
        }
        #endregion
    }
}