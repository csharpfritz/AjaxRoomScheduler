using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxRoomScheduler.Models;
using Telerik.Web.UI;

namespace AjaxRoomScheduler
{
    public partial class Default : System.Web.UI.Page 
    {
        private RoomSchedulerContext _ThisContext;

        protected override void OnInit(EventArgs e)
        {
            Context.Items["Title"] = "Home";

            this._ThisContext = new RoomSchedulerContext();

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetTodaysOccupancyPct();
        }
   
        private void SetTodaysGuests()
        {
            var beginDay = DateTime.Today;
            var endDay = DateTime.Today.AddDays(1).AddSeconds(-1);
            var todaysRes = _ThisContext.Reservations.Where(r => r.ArrivalDate < endDay && r.DepartureDate >= beginDay).Select(r => new
            {
                ResId = r.ReservationID,
                RoomNum = r.Room.Address,
                GuestName = r.Guest.FirstName + " " + r.Guest.LastName,
                SortName = string.Concat(r.Guest.LastName, r.Guest.FirstName)
            });

            currentGuestsGrid.DataSource = todaysRes.ToList();
        }
   
        private void SetTodaysOccupancyPct()
        {
            var totalRooms = (decimal)_ThisContext.Rooms.Count();
            var beginDay = DateTime.Today;
            var endDay = DateTime.Today.AddDays(1).AddSeconds(-1);
            var roomsOccupied = _ThisContext.Reservations.Where(r => r.ArrivalDate < endDay && r.DepartureDate >= beginDay).Count();
            TodaysOccupancyPct = roomsOccupied / totalRooms;
            this.occupancyGuage.Pointer.Value = TodaysOccupancyPct * 100;
            occupancyPctLabel.Text = TodaysOccupancyPct.ToString("0.0%");
        }

        public decimal TodaysOccupancyPct { get; set; }

        //protected RadRadialGauge occupancyGuage;

        protected void currentGuestsGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SetTodaysGuests();
        }

        protected void currentGuestsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Selected a guest: ");
            Debug.WriteLine(e);
        }

        private void DisplayReservationDetails(int reservationId)
        {
            var res = _ThisContext.Reservations
                                  .Include("Guest")
                                  .Include("Room")
                                  .Include("Charges")
                                  .FirstOrDefault(r => r.ReservationID == reservationId);

            // Exit not if there are no reservations with the id requested
            if (res == null)
                return;

            this.txtFirstName.Text = res.Guest.FirstName;
            this.txtLastName.Text = res.Guest.LastName;
            this.assignedRoom.Text = res.Room.Address;

            this.chargesGrid.DataSource = res.Charges;
            this.chargesGrid.DataBind();

        }
        protected void guestDetailsPanel_Load(object sender, EventArgs e)
        {
            DisplayReservationDetails(-1);
        }
}
}
