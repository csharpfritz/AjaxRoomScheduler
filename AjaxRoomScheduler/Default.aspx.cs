﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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

        //protected Telerik.Web.UI.RadAjaxLoadingPanel loadingPanel;

        protected override void OnInit(EventArgs e)
        {
            Context.Items["Title"] = "Home";

            ToggleReservationDetailsVisibility(false);

            base.OnInit(e);
        }

        private RoomSchedulerContext _ThisContext;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            var ajaxMgr = RadAjaxManager.GetCurrent(this);
            ajaxMgr.AjaxRequest += ajaxMgr_AjaxRequest;

            ajaxMgr.AjaxSettings.AddAjaxSetting(ajaxMgr, guestDetailsPanel);

        }

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

        protected void currentGuestsGrid_NeedDataSource(object sender, 
            GridNeedDataSourceEventArgs e)
        {
            SetTodaysGuests();
        }

        private void SetTodaysGuests()
        {
            var beginDay = DateTime.Today;
            var endDay = DateTime.Today.AddDays(1).AddSeconds(-1);
            var todaysRes = _ThisContext.Reservations
                .Where(r => r.ArrivalDate < endDay && r.DepartureDate >= beginDay)
                .Select(r => new {
                ResId = r.ReservationID,
                RoomNum = r.Room.Address,
                GuestName = r.Guest.FirstName + " " + r.Guest.LastName,
                SortName = string.Concat(r.Guest.LastName, r.Guest.FirstName)
            });

            currentGuestsGrid.DataSource = todaysRes.ToList();
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

            var charges = res.Charges.Select(c => new
            {
                Description = c.Description,
                //Charged = c.Value.ToString("$0.00"),
                Value = c.Value
            });
            chargesGrid.DataSource = charges;
            chargesGrid.Visible = true;
            chargesGrid.DataBind();

            ReservationTotalCharges = charges.Sum(a => a.Value).ToString("0.00");
            ReservationNotes = res.Notes;
        }

        #region Proxy Properties to Visual Controls

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
            get { return notesEditor.Content; }
            set { notesEditor.Content = value; }
        }

        public string ReservationTotalCharges
        {
            set
            {
                lTotalCharges.Text = string.Format("<h4>Total: ${0}</h4>", value);
            }
        }

        #endregion

        void ajaxMgr_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            int resId = Convert.ToInt32(e.Argument);
            this.DisplayReservationDetails(resId);
            ToggleReservationDetailsVisibility(true);

        }

        /// <summary>
        /// Show / hide the reservation details controls
        /// </summary>
        /// <param name="displayControls">TRUE: Show details controls</param>
        private void ToggleReservationDetailsVisibility(bool displayControls)
        {
            var controlsToHide = guestDetailsPanel.Controls;
            foreach (Control ctl in controlsToHide)
            {
                ctl.Visible = (displayControls) ? (ctl.ID != "lNoReservationSelected") : (ctl.ID == "lNoReservationSelected");
            }

        }

}
}
