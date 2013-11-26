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
            String downloadType = context.Request.QueryString["downloadType"] != null ? context.Request.QueryString["downloadType"] .ToString() : null;
            String reportType = context.Request.QueryString["reportType"] != null ? context.Request.QueryString["reportType"].ToString() : null;
            String categorization = context.Request.QueryString["categorization"] != null ? context.Request.QueryString["categorization"].ToString() : null;

            if (downloadType == null) {
                List<Article> ExportItems = context.Session[SessionName.ExportItems.ToString()] as List<Article>;
                if (reportType == null) {
                    if (ExportItems.Any()) {
                        ExportItems.ForEach(i => i.UseStoredValues = true);
                        String path = context.Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER) + selectedTemplate;
                        ExcelExporter exporter = new ExcelExporter(path);
                        exporter.DataSource = ExportItems;
                        exporter.DataBind();

                        FileInfo file = new FileInfo(exporter.TempFile);
                        downloadFile(file, context,file.Name);
                    }
                } else if(reportType == ReportType.Categorized.ToString()) {
                    switch (categorization) {
                        case "Room":
                            break;
                        case "Builiding":
                            break;
                        case "Responsible":
                            break;
                        case "Group":
                            var groupedArticles = ExportItems.Where(a=>a.ArticleGroup != null && a.ArticleGroup.ArticleCategory != null)
                                .GroupBy(a =>a.ArticleGroup.ArticleCategory.Name).Select(g => g.ToList());
                            foreach (List<Article> group in groupedArticles) {
                                String groupName = group.ElementAt(0).ArticleGroup.ArticleCategory.Name;

                                String path = context.Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER) + selectedTemplate;
                                ExcelExporter exporter = new ExcelExporter(path);
                                exporter.DataSource = group;
                                exporter.DataBind();

                                FileInfo file = new FileInfo(exporter.TempFile);
                                downloadFile(file, context, "Export_"+groupName+".xls");
                            }
                            break;
                    }
                }
            } else if(downloadType == DownloadType.Template.ToString()) {
                String path = context.Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER) + selectedTemplate;
                FileInfo file = new FileInfo(path);
                downloadFile(file, context, file.Name);
            }
        }

        private void downloadFile(FileInfo file, HttpContext context, String fileName) {
            try {
                if (file.Exists) {
                    BinaryReader fs = new BinaryReader(file.OpenRead());
                    context.Response.ClearContent();
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
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

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}