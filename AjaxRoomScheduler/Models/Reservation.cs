using System;
using System.ComponentModel.DataAnnotations;

namespace AjaxRoomScheduler.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        [Required]
        public Room Room { get; set; }

        [Required]
        public Guest Guest { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public DateTime BookingDateTime { get; set; }
    }
}