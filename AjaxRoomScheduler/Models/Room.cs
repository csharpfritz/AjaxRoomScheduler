using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AjaxRoomScheduler.Models
{
    public class Room {

        public int RoomID { get; set; }

        [Required]
        public string Address { get; set; }
        
        [Required]
        public string BedType { get; set; }

    }

}