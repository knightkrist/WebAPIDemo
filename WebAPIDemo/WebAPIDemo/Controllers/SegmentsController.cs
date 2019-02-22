using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIDemo.Data;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class SegmentsController : ApiController
    {
        private TripContext db = new TripContext();

        // GET: api/Segments
        //public IQueryable<Segment> GetSegments()
        //{
        //    return db.Segments;
        //}

        // GET: api/Segments (Cusomt Controller)
        public IEnumerable<OutputResult> GetSegmentsTrip()
        {

            var items = db.Segments.AsEnumerable().Select(c => {

                string name = db.Trips.Where(i => i.Id == c.TripId).Select(i => i.Name).Single();

                return new OutputResult()
                {
                    Name = c.Name,
                    TripId = c.TripId,
                    TripName = name,
                    Description = c.Description
                };
            }).ToArray();


            return items;

        }

        // GET: api/Segments/5
        [ResponseType(typeof(OutputResult))]
        public IHttpActionResult GetSegment(int id)
        {
            int tripId = db.Segments.Where(i => i.Id == id).Select(i => i.TripId).First();

            string name = db.Trips.Where(i => i.Id == tripId).Select(i => i.Name).Single();

            Segment segment = db.Segments.Find(id);

            var item = new OutputResult()
            {
                Name = segment.Name,
                TripId = segment.TripId,
                TripName = name,
                Description = segment.Description
            };

            if (segment == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Segments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSegment(int id, Segment segment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != segment.Id)
            {
                return BadRequest();
            }

            db.Entry(segment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SegmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Segments
        [ResponseType(typeof(Segment))]
        public IHttpActionResult PostSegment(Segment segment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Segments.Add(segment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = segment.Id }, segment);
        }

        // DELETE: api/Segments/5
        [ResponseType(typeof(Segment))]
        public IHttpActionResult DeleteSegment(int id)
        {
            Segment segment = db.Segments.Find(id);
            if (segment == null)
            {
                return NotFound();
            }

            db.Segments.Remove(segment);
            db.SaveChanges();

            return Ok(segment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SegmentExists(int id)
        {
            return db.Segments.Count(e => e.Id == id) > 0;
        }
    }
}