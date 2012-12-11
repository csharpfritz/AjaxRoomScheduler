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
            var currentItem = siteMenu.FindItemByUrl(Request.Url.PathAndQuery);
            if (currentItem != null)
            {
                currentItem.HighlightPath();
                DataBindBreadCrumbSiteMap(currentItem);
            }
        }
  
        private void DataBindBreadCrumbSiteMap(Telerik.Web.UI.RadMenuItem currentItem)
        {
            var path = new List<RadMenuItem>();
            while (currentItem != null)
            {
                path.Insert(0, currentItem);
                currentItem = currentItem.Owner as RadMenuItem;
            }

            breadCrumbSiteMap.DataSource = path;
            breadCrumbSiteMap.DataBind();
        }
    }
}