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

        protected void guestDetailsPanel_Load(object sender, EventArgs e)
        {
            DisplayReservationDetails(-1);
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

            ReservationDetailsAssignedRoom = string.Format("{0} - Type: {1}", res.Room.Address, res.Room.BedType.ToString());
            ReservationDetailsFirstName = res.Guest.FirstName;
            ReservationDetailsLastName = res.Guest.LastName;

            chargesGrid.DataSource = res.Charges.Select(c => new {
                Description = c.Description,
                Value=c.Value.ToString("$0.00")
            });
            chargesGrid.DataBind();

        }

        public string ReservationDetailsLastName
        {
            get { return txtLastName.Text; }
            set { txtLastName.Text = value; }
        }

        public string ReservationDetailsFirstName
        {
            get { return txtFirstName.Text; }
            set { txtFirstName.Text = value; }
        }

        public string ReservationDetailsAssignedRoom
        {
            get { return assignedRoom.Text; }
            set { assignedRoom.Text = value; }
        }

        public string ReservationNotes
        {
            get { return notesEditor.Text; }
            set { notesEditor.Text = value; }
        }

        protected void currentGuestsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtLastName.Text = "Loaded";
            this.txtFirstName.Text = DateTime.Now.ToString();
        }

        protected void currentGuestsGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            this.txtLastName.Text = "Item Loaded";
            this.txtFirstName.Text = DateTime.Now.ToString();

            var dataItem = e.Item as GridDataItem;
            this.DisplayReservationDetails(Convert.ToInt32( dataItem.GetDataKeyValue("ResId")));
            dataItem.Selected = true;

        }

}
}
