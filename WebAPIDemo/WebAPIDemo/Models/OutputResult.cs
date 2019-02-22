using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemo.Models
{
    public class OutputResult
    {
        public string Name { get; set; }

        public int TripId { get; set; }

        public string TripName { get; set; }

        public string Description { get; set; }
    }
}