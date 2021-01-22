using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrzesiukiewiczL4
{
    public partial class SG_AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            //    Response.Redirect("~/SG_MainPage.aspx");
            // TODO - if not admin redirect to user panel, if not logged in, redirect to login page
        }
    }
}