using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FullCalendar.Models;
using Doodlelu2.Models;


namespace FullCalendar.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        HugDBEntities db = new HugDBEntities();
        // GET: /Calendar/

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetCalendarEvents() {
            List<CalendarEvent> eventItems = new List<CalendarEvent>();
            int i = 0, n = 9;
            for (i = 0; i < n; i++) {
                AddItem(eventItems);
            }

            return Json(eventItems, JsonRequestBehavior.AllowGet);
        }

        Random random = new Random();
        public void AddItem(List<CalendarEvent> eventItems) {
            CalendarEvent item = new CalendarEvent();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, random.Next(1, 30));

            item.id = random.Next(1, 100);
            item.start = startDate.ToString("s");
            item.end = startDate.AddDays(random.Next(1, 5)).ToString("s");
            item.allDay = true;
            item.color = "blue";
            item.title = "Calendar Item " + item.id;
            eventItems.Add(item);
        }
        public ActionResult takvım()
        {
            return View();
        }

    }
}
