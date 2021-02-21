using EIHTestPortal.Helpers;
using EIHTestPortal.Interfaces;
using EIHTestPortal.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EIHTestPortal.Controllers
{
    public class ContactController : Controller
    {

        readonly IContactService _contactService;
        Logger _logger;

        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
            _logger = new Logger();
        }
        // GET: Contact
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authenticate");
            }

            var contacts = _contactService.GetAll();
            ViewData.Model = contacts;
            return View();
        }

        public ActionResult Edit()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Authenticate");
            }
            var strId=Request.Params.Get("strId");
            Contact empty = new Contact();
            if (string.IsNullOrEmpty(strId))
            {
                //it means new contact needs to be added
                
                ViewData.Model = empty;
            }
            else
            {
                //update existing contact
                empty= _contactService.GetById(strId);

                ViewData.Model = empty;

            }
            return View(empty);
        }

        [HttpPost]
        public JsonResult Save(Contact data)
        {
            _logger.LogError("Save function"+data.ToJson());
            bool result = false;
            if(string.IsNullOrEmpty(data.Id))
            {
                //add new contact
                result=_contactService.AddContact(data);
            }
            else
            {
                //update existing contact
                result=_contactService.UpdateContact(data);
            }
            _logger.LogError("Save function,result=" + result);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(Contact data)
        {
            _logger.LogError("Delete function" + data.ToJson());
            bool result = false;
            result = _contactService.DeleteContact(data);
            _logger.LogError("Delete function,result=" + result);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}