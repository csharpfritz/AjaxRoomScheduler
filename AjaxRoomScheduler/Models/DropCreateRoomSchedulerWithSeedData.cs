using System.Data.Entity;

namespace AjaxRoomScheduler.Models
{
    public class DropCreateRoomSchedulerWithSeedData : DropCreateDatabaseAlways<RoomSchedulerContext>
    {
        protected override void Seed(RoomSchedulerContext context)
        {
            var bedTypes = new[] { "2 TWIN", "QUEEN", "KING" };

            for (int floor = 1; floor <= 10; floor++)
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