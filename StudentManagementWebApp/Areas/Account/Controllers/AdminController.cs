using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Areas.Account.Controllers
{
    [RouteArea("Account")]
    public class AdminController : Controller
    {
        // GET: Account/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}