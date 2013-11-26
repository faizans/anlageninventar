using Client.SiteMaster;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Controls.Menu {
    public partial class MenuControl : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {

        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #region Events

        protected void lbMenuItemClicked(object sender, EventArgs e) {
            LinkButton menuItem = (LinkButton)sender;
            string title = null;
            //Load the datasources regarding to commandname and send to reportview
            switch (menuItem.CommandName) {
            
                //-----------------------------INVENTORY REPORTS----------------------------------------//
                case "DeletedArticles_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetDeletedWithRestValue().ToList();
                    title = "Gelöschte Artikel";
                    break;
                case "AllArticles_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetAvailable().ToList();
                    title = "Inventar - Alle Artikel";
                    break;
                case "RoomChecklist_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetAllSortedByUsers();
                    title = "Inventar - Raumchecklist";
                    break;
                case "NotAvailableArticles_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetLost().ToList();
                    title = "Inventar - Nicht vorhandene Artikel";
                    break;
                case "BigReport_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetAllNotDeleted().ToList();
                    title = "Inventar - Alle Artikel";
                    this.SiteMaster.UngroupedReportDataSource = this.SiteMaster.ReportDataSource;
                    Response.Redirect("~/Site/Administrator/Report/BigReport.aspx?title="+title);
                    break;
                case "Entrances_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetAll().ToList();
                    title = "Inventar - Zugänge";
                    this.SiteMaster.UngroupedReportDataSource = this.SiteMaster.ReportDataSource;
                    Response.Redirect("~/Site/Administrator/Report/InAndOutReportView.aspx?type=in&title="+title);
                    break;
                case "Disposals_Inventory":
                    this.SiteMaster.ReportDataSource = Article.GetLostOrDeleted().ToList();
                    title = "Inventar - Abgänge";
                    this.SiteMaster.UngroupedReportDataSource = this.SiteMaster.ReportDataSource;
                    Response.Redirect("~/Site/Administrator/Report/InAndOutReportView.aspx?type=out&title="+title);
                    break;

                //-----------------------------ACCOUNTING REPORTS----------------------------------------//
                case "AllArticles_Accounting":
                    this.SiteMaster.ReportDataSource = Article.GetAvailable().Where(a => a.DepreciationCategory != null && a.DepreciationCategory.DepreciationSpan > 0).ToList();
                    title = "Anlagebuchhaltung - Alle Artikel";
                    break;
                case "Entrances_Accounting":
                    this.SiteMaster.ReportDataSource = Article.GetAll().Where(a => (a.DepreciationCategory != null && a.DepreciationCategory.DepreciationSpan > 0
                        && a.AcquisitionDate.Value.Year < (a.DepreciationCategory.DepreciationSpan + DateTime.Now.Year))).ToList();
                    title = "Anlagebuchhaltung - Zugänge";
                    this.SiteMaster.UngroupedReportDataSource = this.SiteMaster.ReportDataSource;
                    Response.Redirect("~/Site/Administrator/Report/InAndOutReportView.aspx?type=in&title="+title);
                    break;
                case "Disposals_Accounting":
                    this.SiteMaster.ReportDataSource = Article.GetLostOrDeleted().Where(a => (a.DepreciationCategory != null && a.DepreciationCategory.DepreciationSpan > 0 
                        && a.AcquisitionDate.Value.Year < (a.DepreciationCategory.DepreciationSpan + DateTime.Now.Year))).ToList();
                    title = "Anlagebuchhaltung - Abgänge";
                    this.SiteMaster.UngroupedReportDataSource = this.SiteMaster.ReportDataSource;
                    Response.Redirect("~/Site/Administrator/Report/InAndOutReportView.aspx?type=out&title="+title);
                    break;
                case "IT_Accounting":
                    this.SiteMaster.ReportDataSource = Article.GetAvailable().Where(a => a.DepreciationCategory != null && a.DepreciationCategory.Name == "IT").ToList();
                    title = "Anlagebuchhaltung - IT";
                    break;
                case "Mobiliar_Accounting":
                    this.SiteMaster.ReportDataSource = Article.GetAvailable().Where(a => a.DepreciationCategory != null && a.DepreciationCategory.Name == "Mobiliar").ToList();
                    title = "Anlagebuchhaltung - Mobiliar";
                    break;
            }

            this.SiteMaster.UngroupedReportDataSource = this.SiteMaster.ReportDataSource;
            Response.Redirect("~/Site/Administrator/Report/ReportView.aspx?title="+title);
        }

        #endregion
    }
}