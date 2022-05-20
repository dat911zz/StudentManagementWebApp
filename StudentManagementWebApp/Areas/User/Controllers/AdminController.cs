using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Areas.User.Controllers
{
    [RouteArea("User")]
    public class AdminController : Controller
    {
        // GET: User/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}