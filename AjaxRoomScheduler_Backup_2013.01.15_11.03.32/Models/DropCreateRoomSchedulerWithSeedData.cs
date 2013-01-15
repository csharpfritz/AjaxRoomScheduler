using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AjaxRoomScheduler.Models
{
    public class DropCreateRoomSchedulerWithSeedData : DropCreateDatabaseAlways<RoomSchedulerContext>
    {
        protected override void Seed(RoomSchedulerContext context)
        {
            CreateRooms(context);

            CreateGuests(context);

            context.SaveChanges();

            CreateReservations(context);
        }
  
        private void CreateReservations(RoomSchedulerContext context)
        {

            var rdm = new Random();

            int roomId = rdm.Next(1, 10);
            context.Reservations.Add(new Reservation
            {
                ReservationID = 1,
                Guest = context.Guests.First(g => g.GuestID == 1),
                Room = context.Rooms.First(r => r.RoomID == roomId),
                ArrivalDate = DateTime.Today.AddDays(-1),
                BookingDateTime = DateTime.Now.AddMonths(-1),
                DepartureDate = DateTime.Today.AddDays(1),
                Charges = new List<RoomCharge>()
                {
                    new RoomCharge {RoomChargeId=1, Description="Room Charge (3 days)", Value=395.95M},
                    new RoomCharge {RoomChargeId=2, Description="Dinner", Value=48.95M}
                }
            });

            roomId = rdm.Next(11, 20);
            context.Reservations.Add(new Reservation
            {
                ReservationID = 2,
                Guest = context.Guests.First(g => g.GuestID == 2),
                Room = context.Rooms.First(r => r.RoomID == roomId),
                ArrivalDate = DateTime.Today,
                BookingDateTime = DateTime.Now.AddMonths(-1).AddDays(rdm.Next(10)),
                DepartureDate = DateTime.Today.AddDays(2)
            });

            roomId = rdm.Next(21, 30);
            context.Reservations.Add(new Reservation
            {
                ReservationID = 3,
                Guest = context.Guests.First(g => g.GuestID == 3),
                Room = context.Rooms.First(r => r.RoomID == roomId),
                ArrivalDate = DateTime.Today.AddDays(-2),
                BookingDateTime = DateTime.Now.AddMonths(-1).AddDays(rdm.Next(10)),
                DepartureDate = DateTime.Today
            });

            roomId = rdm.Next(31, 40);
            context.Reservations.Add(new Reservation
            {
                ReservationID = 4,
                Guest = context.Guests.First(g => g.GuestID == 4),
                Room = context.Rooms.First(r => r.RoomID == roomId),
                ArrivalDate = DateTime.Today.AddDays(-3),
                BookingDateTime = DateTime.Now.AddMonths(-1).AddDays(rdm.Next(20)),
                DepartureDate = DateTime.Today.AddDays(1)
            });


        }
  
        private void CreateGuests(RoomSchedulerContext context)
        {

            context.Guests.Add(new Guest { GuestID = 1, FirstName = "Bill", LastName = "Lumbergh" });
            context.Guests.Add(new Guest { GuestID = 2, FirstName = "Tony", LastName = "Pepperoni" });
            context.Guests.Add(new Guest { GuestID = 3, FirstName = "Joseph", LastName = "Bag O'Donuts" });
            context.Guests.Add(new Guest { GuestID = 4, FirstName = "Dwight", LastName = "Shrute" });
            context.Guests.Add(new Guest { GuestID = 5, FirstName = "Theo", LastName = "Stig" });

        }
  
        private void CreateRooms(RoomSchedulerContext context)
        {
            var bedTypes = new[] { "2 TWIN", "QUEEN", "KING" };

            for (int floor = 1; floor <= 4; floor++)
            {
                for (int roomNum = 1; roomNum <= 10; roomNum++)
                {
                    var roomId = (floor - 1) * 10 + roomNum;
                    var address = (floor * 10 + roomNum).ToString();
                    var bedType = roomNum < 6 ? bedTypes[0] : roomNum < 8 ? bedTypes[1] : bedTypes[2];
                    context.Rooms.Add(new Room { RoomID = roomId, Address = address, BedType = bedType });
                }
            }
        }
    }
}