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
        [Display(Name = "From (dd/mm-YYYY H:i)", Description = "When the reservation starts")]
        public string DateTimeStart { get; set; }
        [Required]
        [Display(Name = "To (dd/mm-YYYY H:i)", Description = "When the reservation ends")]
        public string DateTimeEnd { get; set; }
        public int State { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VenueId { get; set; }
        [Required]
        [Display(Name = "Table", Description = "The table that will be reserved")]
        public int TableId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        [Required]
        [Display(Name = "Table packages", Description = "Table packages that will be payed upon arrival")]
        public List<string> TablePackageIds { get; set; }
    }
}