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

        protected override void OnInit(EventArgs e)
        {
            Context.Items["Title"] = "Home";

            base.OnInit(e);
        }

        private RoomSchedulerContext _ThisContext;

        protected void Page_Load(object sender, EventArgs e)
        {

            // Begin a data context - connection to the database
            this._ThisContext = new RoomSchedulerContext();

            SetTodaysOccupancyPct();
        }
   
        private void SetTodaysOccupancyPct()
        {
            var totalRooms = (decimal)_ThisContext.Rooms.Count();
            var beginDay = DateTime.Today;
            var endDay = DateTime.Today.AddDays(1).AddSeconds(-1);
            var roomsOccupied = _ThisContext.Reservations.Where(r => 
                r.ArrivalDate < endDay && r.DepartureDate >= beginDay)
                .Count();
            var todaysOccupancyPct = roomsOccupied / totalRooms;
            this.occupancyGuage.Pointer.Value = todaysOccupancyPct * 100;
            occupancyPctLabel.Text = string.Format("{0} occupied - {1} rooms available", todaysOccupancyPct.ToString("0.0%"), totalRooms-roomsOccupied);
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

        //public decimal TodaysOccupancyPct { get; set; }

        //protected RadRadialGauge occupancyGuage;

        protected void currentGuestsGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SetTodaysGuests();
        }

}
}
