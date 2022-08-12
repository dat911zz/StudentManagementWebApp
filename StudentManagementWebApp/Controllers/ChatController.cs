using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementWebApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        [AuthorizeRole(Roles = "ADMIN, MODERATOR, USER")]
        public ActionResult Index()
        {
            return View();
        }
    }
}