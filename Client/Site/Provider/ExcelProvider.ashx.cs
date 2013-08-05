using Client.Util;
using Data.Enum;
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

            String selectedTemplate = context.Request.QueryString["template"].ToString();

            List<Article> ExportItems = context.Session[SessionName.ExportItems.ToString()] as List<Article>;
            if (ExportItems.Any()) {
                String path = context.Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER) + selectedTemplate;
                ExcelExporter exporter = new ExcelExporter(path);
                exporter.DataSource = ExportItems;
                exporter.DataBind();

                FileInfo file = new FileInfo(exporter.TempFile);

                try {
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
                } catch (Exception e) {
                    context.Response.Write(e.Message);
                    context.Response.Flush();
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