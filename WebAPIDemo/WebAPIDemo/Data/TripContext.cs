using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIDemo.Models;

namespace WebAPIDemo.Data
{
    public class TripContext : DbContext
    {
        public TripContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Segment> Segments { get; set; }
    }
}