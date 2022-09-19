using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Utilites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentManagementWebApp.Controllers
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
        public ActionResult Register(Models.User _user)
        {
            if (ModelState.IsValid)
            {
                var checkMail = usersService.GetAll()
                    .Where(x =>
                    x.Email.Equals(_user.Email.ToString())
                    )
                    .FirstOrDefault();
                var checkUsername = usersService.GetAll()
                    .Where(x =>
                    x.UserName.Equals(_user.UserName.ToString())
                    )
                    .FirstOrDefault();

                if (checkMail == null && checkUsername == null)
                {
                    //_user.Hash = GetMD5(_user.Password);
                    _user.Hash = EncryptUsingCertificate(_user.Password);
                    usersService.Add(_user);
                    return RedirectToAction("Index");
                }
                else
                {
                    if (checkMail != null)
                    {
                        ViewBag.errorEmail = "Mail đã tồn tại!";                  
                    }
                    if (checkUsername != null)
                    {
                        ViewBag.errorUsername = "Tên đăng nhập đã tồn tại!";
                    }                 
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
        public ActionResult Login(string username, string password)
        {
            
            if (ModelState.IsValid)
            {
                Models.User data = new Models.User();
                //Hashing and compare with DB
                //var f_password = GetMD5(password);
                var f_password = EncryptUsingCertificate(password);
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
                    //Session["Username"] = data.UserName;
                    //Session["Role"] = "User";
                    //if (data.Manager == true)
                    //{
                    //    Session["Role"] = "Manager";
                    //}
                    FormsAuthentication.SetAuthCookie(data.UserName, false);
                    var authTicket = new FormsAuthenticationTicket(1, data.UserName, DateTime.Now, DateTime.Now.AddMinutes(1), false, data.RoleId);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất cmn bại, vui lòng thử lại =))";
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
        public static string EncryptUsingCertificate(string data)
        {
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath , "/vendor/certificates/mycert.pem");
                var collection = new X509Certificate2Collection();
                collection.Import(path);
                var certificate = collection[0];
                var output = "";
                using (RSA csp = (RSA)certificate.PublicKey.Key)
                {
                    byte[] bytesEncrypted = csp.Encrypt(byteData, RSAEncryptionPadding.OaepSHA1);
                    output = Convert.ToBase64String(bytesEncrypted);
                }
                return output;
            }
            catch (Exception ex)
            {
                return ex.Message + "";
            }
        }
        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }
    }
}