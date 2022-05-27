using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentManagementWebApp.Areas.Account.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUsersService usersService;
        public AuthenticationController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        // GET: Authentication
        public ActionResult Index()
        {
            return View("Login");
        }
        //GET: Register
        public ActionResult Register()
        {
            return View();
        }
        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.User _user)
        {
            if (ModelState.IsValid)
            {
                var check = usersService.GetAll()
                    .Where(x =>
                    x.Email.Equals(_user.Email.ToString())
                    )
                    .FirstOrDefault();

                if (check == null)
                {
                    _user.Hash = GetMD5(_user.Password);
                    usersService.Add(_user);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                Models.User data = new Models.User();
                //Hashing and compare with DB
                var f_password = GetMD5(password);
                //var data = _db.Users.Where(s => s.UserName.Equals(email) && s.Password.Equals(f_password)).ToList();
                data = usersService.GetAll()
                    .Where(x =>
                    x.UserName.Equals(username.ToString()) && x.Hash.Equals(f_password)
                    )
                    .FirstOrDefault();
                //Authencation
                if (data != null)
                {
                    string fullname = data.FirstName + " " + data.LastName;
                    //add session
                    Session["Username"] = data.UserName;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất cmn bại, vui lòng thử lại =))";
                    //return RedirectToAction("Index");
                    return View("Login");
                }
            }
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index","Home");
        }

    }
}