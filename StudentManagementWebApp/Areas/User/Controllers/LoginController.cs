using StudentManagementWebApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        // GET: User/Login
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Login")]
        public ActionResult Login(string a)
        {
            ViewBag.Message = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", a, DateTime.Now.ToString());
            return View("Login");
        }

    }
}