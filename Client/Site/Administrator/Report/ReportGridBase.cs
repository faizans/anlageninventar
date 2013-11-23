using Client.Site.Controls.CustomGrids;
using Client.SiteMaster;
using Client.Util;
using Data.Enum;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Administrator.Report {
    public class ReportGridBase : System.Web.UI.Page {

        #region Events

        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            if (!IsPostBack) {
                this.SelectedTelerikTemplate = null;
                this.SelectedTemplate = null;
                this.ReportYear = DateTime.Now.Year;
                this.InOutYear = DateTime.Now.Year;
                this.GroupReport = false;
            }

            SiteMaster.StandardMaster.InfoText = "Report";
        }

        #endregion

        #region Properties

        protected bool GroupReport {
            get {
                if (Session[SessionName.GroupReport.ToString()] == null) {
                    Session[SessionName.GroupReport.ToString()] = false;
                }
                return (bool)Session[SessionName.GroupReport.ToString()];
            }
            set {
                Session[SessionName.GroupReport.ToString()] = value;
            }
        }

        protected int ReportYear {
            get {
                if (Session[SessionName.ReportYear.ToString()] == null) {
                    Session[SessionName.ReportYear.ToString()] = DateTime.Now.Year;
                }
                return (int)Session[SessionName.ReportYear.ToString()];
            }
            set {
                Session[SessionName.ReportYear.ToString()] = value;
            }
        }

        protected int InOutYear {
            get {
                if (Session[SessionName.InOutYear.ToString()] == null) {
                    Session[SessionName.InOutYear.ToString()] = DateTime.Now.Year;
                }
                return (int)Session[SessionName.InOutYear.ToString()];
            }
            set {
                Session[SessionName.InOutYear.ToString()] = value;
            }
        }

        protected String SelectedTelerikTemplate {
            get {
                if (Session[SessionName.SelectedTelerikTemplate.ToString()] == null) {
                    Session[SessionName.SelectedTelerikTemplate.ToString()] = Constants.TELERIK_REPORT_TEMPLATES[0];
                }
                return Session[SessionName.SelectedTelerikTemplate.ToString()].ToString();
            }
            set {
                Session[SessionName.SelectedTelerikTemplate.ToString()] = value;
            }
        }

        protected String SelectedTemplate {
            get {
                if (Session[SessionName.SelectedTemplate.ToString()] == null) {
                    Session[SessionName.SelectedTemplate.ToString()] = ExcelExporter.GetTemplateFiles(Server)[0].Name;
                }
                return Session[SessionName.SelectedTemplate.ToString()].ToString();
            }
            set {
                Session[SessionName.SelectedTemplate.ToString()] = value;
            }
        }

        /// <summary>
        /// Returns title for report which is coming from menu control
        /// </summary>
        protected String ReportTitle {
            get {
                return Request.Params["title"];
            }
        }

        protected string ReportType {
            get {
                return Request.QueryString["type"] != null ? Request.QueryString["type"] : null;
            }
        }

        protected CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #endregion

        #region Methods

        protected void bindData(RadGrid rgReport) {

            if (this.ReportType == "in") {
                this.SiteMaster.ReportDataSource = this.SiteMaster.ReportDataSource.Where(a => a.AcquisitionDate.Value.Year == this.InOutYear).ToList();   
            } else if (this.ReportType == "out") {
                this.SiteMaster.ReportDataSource = this.SiteMaster.ReportDataSource.Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == this.InOutYear)).ToList(); ;
            } 

            rgReport.DataSource = this.SiteMaster.ReportDataSource;
            rgReport.DataBind();

            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmYear = cmdItem.FindControl("rtbYear") as RadNumericTextBox;
            nmYear.Value = this.ReportYear;

            Label lblGroupArticles = cmdItem.FindControl("lblGroupArticles") as Label;
            lblGroupArticles.Text = this.GroupReport ? "Artikel streuen" : "Artikel gruppieren";

            RadComboBox rlbExcelTemplate = cmdItem.FindControl("rcbExcelTemplate") as RadComboBox;
            rlbExcelTemplate.DataSource = ExcelExporter.GetTemplateFiles(Server);
            rlbExcelTemplate.DataBind();
            rlbExcelTemplate.Items.Where(i => i.Text == this.SelectedTemplate).SingleOrDefault().Selected = true;

            RadComboBox rlbTelerikTemplate = cmdItem.FindControl("rcbTelerikReport") as RadComboBox;
            rlbTelerikTemplate.DataSource = Constants.TELERIK_REPORT_TEMPLATES;
            rlbTelerikTemplate.DataBind();
            rlbTelerikTemplate.Items.Where(i => i.Text == this.SelectedTelerikTemplate).SingleOrDefault().Selected = true;
        }

        protected void ToggleGroupSource(RadGrid rgReport) {
            this.GroupReport = !this.GroupReport;
            if (this.GroupReport) {
                this.SiteMaster.ReportDataSource =
                    ArticleGridHelper.GroupReportArticles(ArticleGridHelper.GetReportItems(rgReport, this.SiteMaster.ReportDataSource, false));
            } else {
                this.SiteMaster.ReportDataSource = new List<Article>(this.SiteMaster.UngroupedReportDataSource);
            }
            this.ReCalculateDepreciation(rgReport);
        }

        protected void GenerateFooter(RadGrid rgReport) {
            if (rgReport.MasterTableView.GetItems(GridItemType.Footer) != null && rgReport.MasterTableView.GetItems(GridItemType.Footer).Count() > 0) {
                GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;

                List<Article> ReportItems = ArticleGridHelper.GetReportItems(rgReport, this.SiteMaster.ReportDataSource, false);

                double? total = ReportItems.Sum(i => i.Value);
                double? depTotal = ReportItems.Sum(i => i.DepreciationValue);
                double? avDepTotal = ReportItems.Sum(i => i.AverageDepreciation);
                double? cumDepTotal = ReportItems.Sum(i => i.CumulatedDepreciation);

                footerItem["Value"].Text = Math.Round(total.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["DepreciationValue"].Text = Math.Round(depTotal.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["AverageDepreciation"].Text = Math.Round(avDepTotal.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["CumulatedDepreciation"].Text = Math.Round(cumDepTotal.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["Name"].Text = "Total:";
            }
        }

        protected void ExportToExcel(RadGrid rgReport) {
            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(rgReport, this.SiteMaster.ReportDataSource, true);
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx?template=" + this.SelectedTemplate);
        }

        protected void ExportToPrintView(RadGrid rgReport) {

            List<Article> exportSource = new List<Article>();

            //Dirty workaround
            if (this.ReportType == "in") {
                exportSource = this.SiteMaster.ReportDataSource.Where(a => a.AcquisitionDate.Value.Year == this.InOutYear).ToList();
            } else if (this.ReportType == "out") {
                exportSource = this.SiteMaster.ReportDataSource.Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == this.InOutYear)).ToList(); ;
            } else {
                exportSource = this.SiteMaster.ReportDataSource;
            }

            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(rgReport, exportSource, false);
            Response.Redirect("~/Site/Administrator/Report/PrintView.aspx?template=" + this.SelectedTelerikTemplate + "&year=" + this.ReportYear + "&title=" + this.ReportTitle);
        }

        protected void ReCalculateDepreciation(RadGrid rgReport) {
            this.SiteMaster.ReportDataSource.ForEach(a => a.DepreciationTime = new DateTime(this.ReportYear, 1, 1));
            rgReport.Rebind();
        }

        protected void UpdateReportYear(RadGrid rgReport) {
            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmYear = cmdItem.FindControl("rtbYear") as RadNumericTextBox;
            this.ReportYear = (int)nmYear.Value;
        }

        protected void UpdateInOutYear(RadGrid rgReport) {
            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmInOutYear = cmdItem.FindControl("rtbInOutYear") as RadNumericTextBox;
            this.InOutYear = (int)nmInOutYear.Value;
            this.ReportYear = this.InOutYear;
        }

        protected void fixGridScroll(RadGrid rgReport) {
            rgReport.Width = Unit.Percentage(100);
            rgReport.ClientSettings.Scrolling.AllowScroll = true;
            rgReport.ClientSettings.Scrolling.ScrollHeight = 250;
            rgReport.MasterTableView.Width = Unit.Percentage(120);
        }

        #endregion

    }
}