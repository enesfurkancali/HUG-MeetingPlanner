using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doodlelu2.Models;

namespace Doodlelu2.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        HugDBEntities db = new HugDBEntities();
        // GET: List
        public ActionResult Index()
        {
            //toplantıları listele
            var toplantilar = db.TBLTOPLANTI.ToList();
            return View(toplantilar);
        }
        public ActionResult Delete(string ad)
        { 
            //toplantı sil
            TBLTOPLANTI toplanti = db.TBLTOPLANTI.Where(t => t.topad == ad).FirstOrDefault();
            db.Entry(toplanti).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}