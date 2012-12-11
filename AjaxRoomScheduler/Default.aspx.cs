using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

public partial class Default : System.Web.UI.Page 
{

    protected override void OnInit(EventArgs e)
    {

        Context.Items["Title"] = "Home";
        
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
