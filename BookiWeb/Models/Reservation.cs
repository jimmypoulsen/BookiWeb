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
        [Display(Name = "From", Description = "When the reservation starts")]
        public string DateTimeStart { get; set; }
        [Required]
        [Display(Name = "To", Description = "When the reservation ends")]
        public string DateTimeEnd { get; set; }
        public int State { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VenueId { get; set; }
        [Required]
        public int TableId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public List<string> TablePackageIds { get; set; }
    }
}