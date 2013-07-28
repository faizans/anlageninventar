using Client.Util;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Client.Site.Provider {
    /// <summary>
    /// Summary description for ExcelProvider
    /// </summary>
    public class ExcelProvider : IHttpHandler ,IRequiresSessionState{

        public void ProcessRequest(HttpContext context) {
            List<Article> ExportItems = context.Session["ExportItems"] as List<Article>;
            if (ExportItems.Any()) {
                String path = context.Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER) + Constants.EXCEL_TEMPLATE_NAME;
                ExcelExporter exporter = new ExcelExporter(path);
                exporter.DataSource = ExportItems;
                exporter.DataBind();

                FileInfo file = new FileInfo(exporter.TempFile);
                if (file.Exists) {
                    BinaryReader fs = new BinaryReader(file.OpenRead());
                    context.Response.ClearContent();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + exporter.FileName);
                    context.Response.AddHeader("Content-Length", file.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    byte[] bite = fs.ReadBytes((int)file.Length);
                    fs.Close();
                    context.Response.BinaryWrite(bite);
                    context.Response.Flush();
                } else {
                    context.Response.Write("This file does not exist.");
                }

            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}