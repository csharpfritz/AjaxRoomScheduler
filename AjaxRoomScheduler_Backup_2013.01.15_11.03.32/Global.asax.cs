using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AjaxRoomScheduler.Models;

namespace AjaxRoomScheduler
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new DropCreateRoomSchedulerWithSeedData());
            Room11Test();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        static void Room11Test()
        {

            using (var context = new RoomSchedulerContext())
            {
                var room = from r in context.Rooms
                           where r.Address == "11"
                           select r;

                if (room.Count() == 1)
                {
                    Debug.WriteLine("Test passed, found room 11");
                }
                else
                {
                    Debug.WriteLine("Test Failed");
                }

            }

        }

    }
}