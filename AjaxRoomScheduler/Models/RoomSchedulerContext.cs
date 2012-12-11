using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AjaxRoomScheduler.Models
{

    public class RoomSchedulerContext : DbContext
    {

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

    }

}