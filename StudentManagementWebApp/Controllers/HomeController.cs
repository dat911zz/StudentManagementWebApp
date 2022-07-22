using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Controllers
{
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Intro()
        {
            ViewBag.Message = "Intro page";
            return View();
        }
        [AuthorizeRole(Role.ADMIN, Role.SUPERADMIN, Role.USER)]
        public ActionResult Chat()
        {
            return RedirectToAction("Index", "Chat");
        }
    }
}