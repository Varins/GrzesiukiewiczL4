using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrzesiukiewiczL4
{
    public partial class SG_MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated))
                Response.Redirect("~/SG_Login.aspx");
        }
    }
}