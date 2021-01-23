using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace GrzesiukiewiczL4
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // Check if files in folder correspond to the filelist in database
            // if file is missing - delete record from database
            ArrayList SG_filelist = SG_DBManager.dbFileList();
            string SG_serverPath = Server.MapPath("./SG_Pliki/");
            foreach (string SG_filename in SG_filelist)
            {
                string SG_filePath = SG_serverPath + SG_filename;
                if (!File.Exists(SG_filePath))
                {
                    SG_DBManager.deleteFileRecord(SG_filename);
                }
            }
        }
    }
}