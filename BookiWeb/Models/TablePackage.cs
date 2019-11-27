using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookiWeb.Models {
    public class TablePackage {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int VenueId { get; set; }

    }
}