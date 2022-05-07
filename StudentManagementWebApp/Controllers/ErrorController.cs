using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult ItemNotFound()
        {
            return View("ItemNotFound");
        }
    }
}