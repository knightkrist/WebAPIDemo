using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAPIDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class SegmentsTripsController : Controller
    {
        private TripContext db = new TripContext();

        // GET: SegmentsTrips
        [HttpGet]
        public ActionResult Index()
        {
            var items = db.Segments.AsEnumerable().Select(c =>
            {

                string name = db.Trips.Where(i => i.Id == c.TripId).Select(i => i.Name).Single();

                return new OutputResult()
                {
                    SegmentId = c.Id,
                    Name = c.Name,
                    TripId = c.TripId,
                    TripName = name,
                    Description = c.Description
                };
            }).ToList();

            return View(items);
        }


        // Create Template : Create SegmentsTrips
        public ActionResult Create()
        {
            //ViewBag.Message = "Create Segments";

            List<Trip> trip = db.Trips.ToList();

            ViewBag.trip_list = new SelectList(trip, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(OutputResult oresult)
        {
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

            //return View();
        }

        // GET: SegmentsTrips/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int tripId = db.Segments.Where(i => i.Id == id).Select(i => i.TripId).First();

            string name = db.Trips.Where(i => i.Id == tripId).Select(i => i.Name).Single();

            Segment segment = await db.Segments.FindAsync(id);

            var item = new OutputResult()
            {
                SegmentId = segment.Id,
                Name = segment.Name,
                TripId = segment.TripId,
                TripName = name,
                Description = segment.Description
            };

            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: SegmentsTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OutputResult oresult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oresult).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(oresult);
        }
    }
}