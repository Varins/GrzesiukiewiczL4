using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace GrzesiukiewiczL4
{
    public partial class SG_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Membership.CreateUser("admin", "123_qaz");
            //Roles.CreateRole("Admin");
            //Roles.AddUserToRole("admin", "Admin");
            if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("~/SG_MainPage.aspx");

        }
    }
}