using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrzesiukiewiczL4
{
    public partial class SG_UserPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string SG_username = HttpContext.Current.User.Identity.Name;
            SG_username_label.Text = SG_username;
            SG_fileListView.DataBind();
            
            if (!(HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated))
                Response.Redirect("~/SG_Login.aspx");
            else
            {
                if (Roles.IsUserInRole(SG_username, "admin") || Roles.IsUserInRole(SG_username, "Uprzywilejowany"))
                {
                    SG_checkFileCount();
                    SG_resetLabels();
                    SG_mainUserPanel.Visible = true;
                    SG_mainUserPanel.Enabled = true;
                    SG_usernamePanel.Visible = false;
                }
                else
                {
                    SG_mainUserPanel.Visible = false;
                    SG_mainUserPanel.Enabled = false;
                    SG_username_label.Text = "Konto nie zostało zatwierdzone przez administratora.";
                    SG_usernamePanel.Visible = true;
                }
                
            }
        }

        private void SG_checkFileCount()
        {
            if (SG_DBManager.numberOfFilesForUser(HttpContext.Current.User.Identity.Name) >= 2)
            {

                SG_overflowLabel.Visible = true;
                SG_overflowLabel.CssClass = "text-info";
                SG_overflowLabel.Text = "Możesz dodać maksymalnie 2 pliki, zwolnij miejsce, aby dodać plik.";
                SG_fileUpload.Enabled = false;
                SG_uploadButton.Enabled = false;
                SG_uploadButton.CssClass = "btn btn-outline-secondary disabled";
            }
            else
            {
                SG_uploadButton.CssClass = "btn btn-outline-info";
                SG_uploadButton.Enabled = true;
                SG_fileUpload.Enabled = true;
                SG_overflowLabel.Visible = false;
                SG_overflowLabel.Text = "";
            }
        }

        protected void SG_uploadButton_Click(object sender, EventArgs e)
        {
            if (SG_fileUpload.PostedFile.ContentLength > 1024)
            {
                SG_uploadLabel.CssClass = "text-danger";
                SG_uploadLabel.Text = "Rozmiar pliku musi być mniejszy niż 1 KB (obecny rozmiar: " + (SG_fileUpload.PostedFile.ContentLength / 1024) + " KB)";
            }
            else if (SG_fileUpload.HasFile)
            {
                string SG_serverPath = Page.MapPath("./SG_Pliki/"); ;
                string SG_filePath = SG_serverPath + SG_fileUpload.FileName;
                if (!File.Exists(SG_filePath))
                {
                    SG_fileUpload.SaveAs(SG_filePath);
                    SG_DBManager.addRecord(SG_fileUpload.FileName, HttpContext.Current.User.Identity.Name);
                    SG_fileListView.DataBind();
                    SG_uploadLabel.CssClass = "text-primary";
                    SG_uploadLabel.Text = "Pomyślnie dodano plik '" + SG_fileUpload.FileName + "'";
                }
                else
                {
                    SG_uploadLabel.CssClass = "text-warning";
                    SG_uploadLabel.Text = "Plik '" + SG_fileUpload.FileName + "' już znajduje się na serwerze.";
                }
            }
            else
            {
                SG_uploadLabel.CssClass = "text-warning";
                SG_uploadLabel.Text = "Nie wybrano pliku.";
            }
            SG_checkFileCount();
        }

        private void SG_resetLabels()
        {
            SG_uploadLabel.CssClass = "";
            SG_uploadLabel.Text="";
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
            SG_resetLabels();
        }

        protected void SG_LinkButton_delete_Command(object sender, CommandEventArgs e)
        {
            string SG_filename = (string)e.CommandArgument;
            string SG_filepath = Page.MapPath("./SG_Pliki/") + SG_filename;
            FileInfo SG_file = new FileInfo(SG_filepath);
            if (SG_file.Exists)
            {
                SG_file.Delete();
                SG_DBManager.deleteFileRecord(SG_filename);
                SG_fileListView.DataBind();
            }
            SG_resetLabels();
        }
    }
}