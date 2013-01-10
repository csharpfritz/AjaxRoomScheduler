using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace AjaxRoomScheduler
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Identify our location in the RadMenu
            var currentItem = siteMenu.FindItemByUrl(Request.Url.PathAndQuery);
            if (currentItem != null)
            {
                // Configure the breadcrumbs
                DataBindBreadCrumbSiteMap(currentItem);
            }
            else
            {
                var path = new List<RadMenuItem>();
                path.Add(new RadMenuItem("Home", "http://localhost"));
                breadCrumbSiteMap.DataSource = path;
                breadCrumbSiteMap.DataBind();
            }
        }
  
        private void DataBindBreadCrumbSiteMap(Telerik.Web.UI.RadMenuItem currentItem)
        {
            // Walk up the hierarchy of the menu to create a map of our current location
            var path = new List<RadMenuItem>();
            while (currentItem != null)
            {
                path.Insert(0, currentItem);
                currentItem = currentItem.Owner as RadMenuItem;
            }

            // Bind our hierarchy to the RadSiteMap
            breadCrumbSiteMap.DataSource = path;
            breadCrumbSiteMap.DataBind();
        }
    }
}