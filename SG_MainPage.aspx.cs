using System;
using System.IO;
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
            
        }

        protected void SG_LinkButton_download_Command(object sender, CommandEventArgs e)
        {
            string SG_filename = (string)e.CommandArgument;
            string SG_filepath = Page.MapPath("./SG_Pliki/") + SG_filename;
            if (File.Exists(SG_filepath))
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + SG_filename);
                Response.TransmitFile(SG_filepath);
                Response.End();
            }

        }
    }
}