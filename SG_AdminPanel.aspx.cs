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
            SG_username_label.Text = HttpContext.Current.User.Identity.Name;
            if (!(HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated))
                Response.Redirect("~/SG_Login.aspx");
            //IList<ListViewDataItem> SG_rows = SG_userList.Items;
            //foreach(ListViewDataItem SG_tempRow in SG_rows)
            //{
            //    string SG_username = ((Label)SG_tempRow.FindControl("UserNameLabel")).Text;
            //    if(Roles.IsUserInRole(SG_username, "Uprzywilejowany"))
            //    {
            //        ((LinkButton)SG_tempRow.FindControl("SG_LinkButton_register")).Enabled = false;
            //        ((LinkButton)SG_tempRow.FindControl("SG_LinkButton_deregister")).Enabled = true;
            //    }
            //    else
            //    {
            //        ((LinkButton)SG_tempRow.FindControl("SG_LinkButton_register")).Enabled = true;
            //        ((LinkButton)SG_tempRow.FindControl("SG_LinkButton_deregister")).Enabled = false;
            //    }
            //}
        }

        protected void SG_userList_RowCommand(object sender, CommandEventArgs e)
        {
            string SG_username = (string)e.CommandArgument;
            switch (e.CommandName)
            {
                case "register":
                    {
                        if(!Roles.IsUserInRole(SG_username, "Uprzywilejowany"))
                        {
                            Roles.AddUserToRole(SG_username, "Uprzywilejowany");
                            SG_infoLabel.Text = "Użytkownikowi '" + SG_username + "' nadano przywileje!";
                        }
                        else
                        {
                            SG_infoLabel.Text = "Użytkownik '" + SG_username + "' jest już Uprzywilejowany!";

                        }
                        SG_infoLabel.Visible = true;
                        break;
                    }
                case "deregister":
                    {
                        if (Roles.IsUserInRole(SG_username, "Uprzywilejowany"))
                        {
                            Roles.RemoveUserFromRole(SG_username, "Uprzywilejowany");
                            SG_infoLabel.Text = "Użytkownikowi '" + SG_username + "' zabrano przywileje!";
                        }
                        else
                        {
                            SG_infoLabel.Text = "Użytkownik '" + SG_username + "' nie był uprzywilejowany!";
                        }
                        SG_infoLabel.Visible = true;
                        break;
                    }
            }
            Page_Load(sender, e);
        }
    }
}