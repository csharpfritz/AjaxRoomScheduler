using System;
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
        SetTodaysGuests();
    }
  
    private void SetTodaysGuests()
    {

        var beginDay = DateTime.Today;
        var endDay = DateTime.Today.AddDays(1).AddSeconds(-1);
        var todaysRes = _ThisContext.Reservations.Where(r => r.ArrivalDate < endDay && r.DepartureDate >= beginDay).Select(r => new
        {
            RoomNum = r.Room.Address,
            GuestName = r.Guest.FirstName + " " + r.Guest.LastName
        });

        currentGuestsGrid.DataSource = todaysRes.ToList();
        currentGuestsGrid.DataBind();

    }
  
    private void SetTodaysOccupancyPct()
    {
        var totalRooms = (decimal)_ThisContext.Rooms.Count();
        var beginDay = DateTime.Today;
        var endDay = DateTime.Today.AddDays(1).AddSeconds(-1);
        var roomsOccupied = _ThisContext.Reservations.Where(r => r.ArrivalDate < endDay && r.DepartureDate >= beginDay).Count();
        TodaysOccupancyPct = roomsOccupied / totalRooms;
        this.occupancyGuage.Pointer.Value = TodaysOccupancyPct*100;
        occupancyPctLabel.Text = TodaysOccupancyPct.ToString("0.0%");
    }

    public decimal TodaysOccupancyPct { get; set; }

    //protected RadRadialGauge occupancyGuage;

}
