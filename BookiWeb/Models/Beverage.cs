using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiWeb.Models {
    public class Beverage {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int Stock { get; set; }
        public int VenueId { get; set; }

    }
}