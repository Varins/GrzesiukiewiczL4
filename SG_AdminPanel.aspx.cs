using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace GrzesiukiewiczL4
{
    public partial class SG_AdminPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(HttpContext.Current.User != null)
            {
                SG_UserNameLabel.Text = HttpContext.Current.User.IsInRole("admin") ? "admin" : "Nie jesteś administratorem";
            }
            if (!(HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated))
                Response.Redirect("~/SG_Login.aspx");
            // TODO - if not admin redirect to user panel, if not logged in, redirect to login page
        }
    }
}