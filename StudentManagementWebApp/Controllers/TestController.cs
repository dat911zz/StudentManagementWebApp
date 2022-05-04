using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            return PartialView("_MenuBar");
        }
    }
}