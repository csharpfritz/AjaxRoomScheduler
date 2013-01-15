using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AjaxRoomScheduler.Models
{
    public class Guest
    {
        public Guest()
        {
            Reservations = new List<Reservation>();
        }

        public int GuestID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}