using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Doodlelu2.Models;

namespace Doodlelu2.Controllers
{
    public class SecurityController : Controller
    {
        HugDBEntities db = new HugDBEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TBLKULLANICI kullanici)
        {
            var bilgiler = db.TBLKULLANICI.FirstOrDefault(x => x.mail == kullanici.mail && x.sifre == kullanici.sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullaniciad, kullanici.rememberme);
                return RedirectToAction("Homepage", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Girdiğin e-posta ve şifre kayıtlarımızla eşleşmedi. Lütfen doğru girdiğinden emin ol ve tekrar dene.";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}