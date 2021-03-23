using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Doodlelu2.Models;


namespace Doodlelu2.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        HugDBEntities db = new HugDBEntities();
        // GET: Meeting
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TBLTOPLANTI toplanti)
        {
            var bilgiler = db.TBLTOPLANTI.FirstOrDefault(x => x.topad == toplanti.topad);

            if (bilgiler == null)
            {
                db.TBLTOPLANTI.Add(toplanti);
                TempData["davetciad"] = toplanti.davetciad;
                TempData["topad"] = toplanti.topad;
                TempData["platform"] = toplanti.platform;
                TempData["notlar"] = toplanti.notlar;
                return RedirectToAction("Invite", "Meeting");
            }
            else
            {
                ViewBag.Message = "Girdiğiniz toplantı adı kullanılıyor.";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Invite()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Invite(TBLTOPLANTI toplanti)
        {
            db.TBLTOPLANTI.Add(toplanti);
            TempData["davetli1"] = toplanti.davetli1;
            TempData["davetli2"] = toplanti.davetli2;
            TempData["davetli3"] = toplanti.davetli3;
            TempData["davetli4"] = toplanti.davetli4;
            TempData["davetli5"] = toplanti.davetli5;
            TempData["davetli"] = toplanti.davetli;

            
            return RedirectToAction("Index", "Calendar");
        }
        [HttpGet]
        public ActionResult Calendar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Calendar(TBLTOPLANTI toplanti)
        {
            toplanti.platform = (string)TempData["platform"];
            toplanti.topad = (string)TempData["topad"];
            toplanti.davetciad = (string)TempData["davetciad"];
            toplanti.notlar = (string)TempData["notlar"];
            toplanti.davetli1 = (string)TempData["davetli1"];
            toplanti.davetli2 = (string)TempData["davetli2"];
            toplanti.davetli3 = (string)TempData["davetli3"];
            toplanti.davetli4 = (string)TempData["davetli4"];
            toplanti.davetli5 = (string)TempData["davetli5"]; 
            toplanti.davetli = (int)TempData["davetli"];
            db.TBLTOPLANTI.Add(toplanti);
            db.SaveChanges();
           
            //mail gönderme
            MailMessage mailim = new MailMessage();
            switch (toplanti.davetli)
            {
                case 1:
                    mailim.To.Add(toplanti.davetli1);
                    break;
                case 2:
                    mailim.To.Add(toplanti.davetli1);
                    mailim.To.Add(toplanti.davetli2);
                    break;
                case 3:
                    mailim.To.Add(toplanti.davetli1);
                    mailim.To.Add(toplanti.davetli2);
                    mailim.To.Add(toplanti.davetli3);
                    break;
                case 4:
                    mailim.To.Add(toplanti.davetli1);
                    mailim.To.Add(toplanti.davetli2);
                    mailim.To.Add(toplanti.davetli3);
                    mailim.To.Add(toplanti.davetli4);
                    break;      
                default:
                    mailim.To.Add(toplanti.davetli1);
                    mailim.To.Add(toplanti.davetli2);
                    mailim.To.Add(toplanti.davetli3);
                    mailim.To.Add(toplanti.davetli4);
                    mailim.To.Add(toplanti.davetli5);
                    break;
            }
           
            mailim.From = new MailAddress("mailadresi");
            mailim.Subject ="Hug toplantı daveti.";
            if(toplanti.notlar==null)
                mailim.Body = toplanti.davetciad + " tarafından " + toplanti.topad + " isimli toplantıya davet edildiniz.<br>" +
                "Toplantı " + toplanti.toptarih1 + " tarihinde " + toplanti.platform + " üzerinden gerçekleştirilecektir.<br><br>";
            else
                mailim.Body = toplanti.davetciad + " tarafından " + toplanti.topad + " isimli toplantıya davet edildiniz.<br>" +
               "Toplantı " + toplanti.toptarih1 + " tarihinde " + toplanti.platform + " üzerinden gerçekleştirilecektir.<br><br> Notlar <br>"+toplanti.notlar;

            mailim.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("mailadresi","şifre");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(mailim);

            return RedirectToAction("Homepage", "Home");
        }

    }
}