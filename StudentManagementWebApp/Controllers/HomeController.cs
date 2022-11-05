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
        [AllowAnonymous]
        public ActionResult UserManagement()
        {
            //https://localhost:44387/Manage/Index
            string domainName = Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            //Response.Redirect("https://" + domainName + "/Manage/Index");
            return Redirect("https://" + domainName + "/Manage/Index");
            
        }
    }
}