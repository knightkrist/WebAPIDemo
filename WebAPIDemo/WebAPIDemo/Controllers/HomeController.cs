using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPIDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class HomeController : Controller
    {
        private TripContext db = new TripContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Segments()
        {
            
            ViewBag.Message = "Segments List";

            List<Trip> trip = db.Trips.ToList();

            ViewBag.trip_list = new SelectList(trip, "Id", "Name"); 

            return View();
        }

        [HttpPost]
        public ActionResult Segments(OutputResult oresult)
        {

            ViewBag.Message = "Segments List";

            try
            {
                List<Trip> trip = db.Trips.ToList();

                ViewBag.trip_list = new SelectList(trip, "Id", "Name");

                Segment sg = new Segment();

                sg.Name = oresult.Name;
                sg.TripId = oresult.TripId;
                sg.Description = oresult.Description;
                sg.StartDateTime = DateTime.Now;
                sg.EndDateTime = DateTime.Now.AddDays(3);

                db.Segments.Add(sg);
                db.SaveChanges();
                int lastentry = sg.Id;

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }

 //           return View();
        }
    }
}