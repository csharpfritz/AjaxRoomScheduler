using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AjaxRoomScheduler.Models
{
    public class Reservation
    {

        public Reservation()
        {
            Charges = new List<RoomCharge>();
        }
        
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

        public List<RoomCharge> Charges { get; set; }

    }

}