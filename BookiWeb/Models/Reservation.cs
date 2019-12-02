using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookiWeb.Models {
    public class Reservation {
        public int Id { get; set; }
        public int ReservationNo { get; set; }
        [Required]
        public string DateTimeStart { get; set; }
        [Required]
        public string DateTimeEnd { get; set; }
        public int State { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VenueId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}