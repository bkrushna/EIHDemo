using EIHTestPortal.Helpers;
using EIHTestPortal.Models;
using EIHTestPortal.Models.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EIHTestPortal.Controllers
{
    public class AuthenticateController : Controller
    {
        // GET: Authenticate
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                var conn_str = ConfigurationManager.AppSettings["connectionString"];

                DBHelper dbObj = new DBHelper(conn_str);

                var is_present = dbObj.GetUser(objUser.EmailId);
                if (is_present != null)
                {
                    //now check if that user is same or not
                    var hashPassword = SaltHelper.AreEqual(objUser.Password, is_present.Password,is_present.Salt);

                    if (hashPassword)
                    {
                        FormsAuthentication.SetAuthCookie(objUser.EmailId, false);

                        Session["UserID"] = objUser.EmailId;
                        return RedirectToAction("Index", "Contact");
                    }
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View(objUser);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(SignupUser objUser)
        {
            if (ModelState.IsValid)
            {

                var salt = SaltHelper.GetSalt();

                User addObj = new User();
                addObj.EmailAddress = objUser.EmailId;

                var hashPassword = SaltHelper.GenerateHash(objUser.Password ,salt);

                addObj.Password = hashPassword;

                addObj.Salt = salt;

                //REGISTER PROCESS

                var conn_str = ConfigurationManager.AppSettings["connectionString"];

                DBHelper dbObj = new DBHelper(conn_str);

                var is_present = dbObj.GetUser(objUser.EmailId);
                if(is_present==null)
                {
                    dbObj.AddUser(addObj);    
                    return RedirectToAction("Login", "Authenticate");
                }
            }
            
            ModelState.AddModelError("", "User is already present");
            return View("Signup", objUser);
        }
    }
}