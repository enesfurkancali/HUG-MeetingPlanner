using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doodlelu2.Models;

namespace Doodlelu2.Controllers
{
    public class RegisterController : Controller
    {
        HugDBEntities db = new HugDBEntities();
        
        // GET: Register
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(TBLKULLANICI kullanici)
        {
            var bilgiler = db.TBLKULLANICI.FirstOrDefault(x => x.kullaniciad == kullanici.kullaniciad);
            if (bilgiler == null)
            {
                db.TBLKULLANICI.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Login", "Security");

            }
            else
            {
                ViewBag.Message = "Girdiğiniz kullanıcı adı başka bir kullanıcı tarafından kullanılıyor.";
                return View();

            }
        }
    }
}