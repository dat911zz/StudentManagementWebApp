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
        
        private readonly Manager _mng;
        //Chỉ đọc
        private readonly IStudentService _service_sv;
        private readonly ISubjectService _service_mh;
        private readonly ICourseService _service_cs;

        static List<Student> studentList = new List<Student>();
        static List<Subject> subjectList = new List<Subject>();
        static List<Subject> subjectQueue = new List<Subject>();

        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public StudentController(Manager manager, IStudentService studentService, ISubjectService subjectService, ICourseService courseService)
        {
            _mng = manager;
            _service_sv = studentService;
            _service_mh = subjectService;
            _service_cs = courseService;
        }
        [NonAction]
        public void GetData()
        {
            studentList = _service_sv.GetAll();
            studentList.ForEach(x => x.CourseDetail = _service_cs.GetCourse(x.Id));
        }
        #region Index
        // GET: Student
        [AuthorizeRole(Roles = "ADMIN, MODERATOR, USER")]
        public ActionResult Index()
        {           
            try
            {
                //Lấy ds sinh viên + ĐKHP
                GetData();
                //Sort list 
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
                _service_sv.Update(std);

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

            ViewBag.ResultList = Manager.ConvertDataTableToHTML(_mng.UploadSubjectSVIntoDataTable(std));           
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
            ViewBag.CourseList = Manager.ConvertDataTableToHTML(_mng.UploadSubjectSVIntoDataTable(std));

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
            ViewBag.ResultList = Manager.ConvertDataTableToHTML(_mng.UploadScoreSVIntoDataTable(std));
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
                
                _service_sv.Add(std);
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
            _service_sv.Remove(std.Id);
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
            GetData();
            return View("SearchIdToCR");
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
                return View("SearchIdToCR");
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
            try
            {
                var std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
                //Lấy toàn bộ môn học
                subjectList = _service_mh.GetAll();
                //Lấy ra danh sách môn học mà sinh viên đã đăng ký
                var tmpSList = _mng.GetSubjectList(std.CourseDetail.ResultList);

                //Lấy ra danh sách môn học chưa đăng ký
                ViewBag.Subjects = subjectList.Except(tmpSList, new SubjectEComparer()).Except(subjectQueue, new SubjectEComparer());
                //Nạp danh sách môn học trong hàng chờ vào View
                ViewBag.TmpList = subjectQueue;
                return View("CourseRegister", std);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error was sent from [StudentController - DKHP]");
                return RedirectToAction("Expt", "Error", new { mess = ex.Message.ToString() });
            }            
        }
        // Student/InQueue/Id&SubId
        [HttpPost]
        public ActionResult InQueue(string id, string subId)
        {
            Student std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            var picked = new Subject(subjectList.Where(x => x.SubjectId.Equals(subId)).FirstOrDefault());
            if (subjectQueue.Where(x => x.SubjectId.Equals(picked.SubjectId)).FirstOrDefault() == null)
            {
                subjectQueue.Add(picked);
            }
            
            List<Subject> tmpSList = _mng.GetSubjectList(std.CourseDetail.ResultList);
            ViewBag.Subjects = subjectList.Except(tmpSList, new SubjectEComparer());
            ViewBag.TmpList = subjectQueue;

            return View("CourseRegister", std);
        }
        // Student/DeQueue/Id&SubId
        [HttpPost]
        public ActionResult DeQueue(string id, string subId)
        {
            Student std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            var picked = new Subject(subjectList.Where(x => x.SubjectId.Equals(subId)).FirstOrDefault());
            try
            {
                if (subjectQueue.Count >= 0)
                {
                    subjectQueue.RemoveAt(subjectQueue.FindIndex(x => x.SubjectId.Equals(picked.SubjectId)));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error was sent from [StudentController - DeQueue]");
                return RedirectToAction("Expt", "Error", new { mess = ex.Message.ToString() });
            }

            List<Subject> tmpSList = _mng.GetSubjectList(std.CourseDetail.ResultList);
            ViewBag.Subjects = subjectList.Except(tmpSList, new SubjectEComparer());
            ViewBag.TmpList = subjectQueue;

            return View("CourseRegister", std);
        }
        [HttpPost]
        public ActionResult CommitDKHP(string id)
        {
            Student std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            //List<Subject> -> List<Result> ?
            _service_cs.AddCourse(id, subjectQueue);
            subjectQueue.Clear();
            //Đồng bộ hóa với DB
            GetData();

            List<Subject> tmpSList = _mng.GetSubjectList(std.CourseDetail.ResultList);
            ViewBag.Subjects = subjectList.Except(tmpSList, new SubjectEComparer());
            ViewBag.TmpList = subjectQueue;

            return View("CourseRegister", std);
        }
        #endregion

        #region Score Updater
        [HttpGet]
        public ActionResult PreScoreUpdater()
        {
            GetData();
            return View("SearchIdToSU");
        }
        [HttpPost]
        public ActionResult PreScoreUpdater(string id)
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
                return View("SearchIdToSU");
            }

            return RedirectToAction("ScoreUpdater", new { id = std.Id });//Redierect thi phải new cái id nếu không sẽ bị lộn qua controller :v
        }
        [HttpGet]
        public ActionResult ScoreUpdater(string id)
        {
            studentList.ForEach(x => x.CourseDetail = _service_cs.GetCourse(x.Id));
            var std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (std == null)
            {
                return RedirectToAction("ItemNotFound", "Error");
            }
            ViewBag.Id = id;
            //Đẩy kết quả ra view
            ViewBag.ResultList = Manager.ConvertDataTableToHTML(_mng.UploadScoreSVIntoDataTable(std));

            //Bind dropdown
            ViewBag.dropdownList = Manager.ConvertSubjecttListToSelectTag(_mng.GetSubjectList(std.CourseDetail.ResultList));
            return View("ScoreUpdater", std);
        }
        [HttpPost]
        public ActionResult ScoreUpdater(string id, string mmh, float dqt, float dtp)
        {
            var std = studentList.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _service_cs.UpdateScore(id, mmh, dqt, dtp);           
            return RedirectToAction("ScoreUpdater", new { id });
        }
        #endregion
    }
}