using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAPIDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class OutputResultController : Controller
    {
        private TripContext db = new TripContext();

        // GET: OutputResult
        public async Task<ActionResult> Index()
        {
            return View(await db.Segments.ToListAsync());
        }


        // GET: OutputResult/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // GET: OutputResult/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OutputResult/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TripId,Name,Description,StartDateTime,EndDateTime")] Segment segment)
        {
            if (ModelState.IsValid)
            {
                db.Segments.Add(segment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(segment);
        }

        // GET: OutputResult/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // POST: OutputResult/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TripId,Name,Description,StartDateTime,EndDateTime")] Segment segment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(segment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(segment);
        }

        // GET: OutputResult/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = await db.Segments.FindAsync(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // POST: OutputResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Segment segment = await db.Segments.FindAsync(id);
            db.Segments.Remove(segment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
