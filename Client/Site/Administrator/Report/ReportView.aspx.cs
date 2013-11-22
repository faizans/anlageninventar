using Client.SiteMaster;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq.Dynamic;
using Client.Site.Controls.CustomGrids;
using Client.Util;
using Data.Enum;

namespace Client.Site.Administrator {
    public partial class ReportView : System.Web.UI.Page {

        #region Properties

        private int ReportYear {
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

        private String SelectedTelerikTemplate {
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

        private String SelectedTemplate {
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
        public String ReportTitle {
            get {
                return Request.Params["title"];
            }
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            if (!IsPostBack) {
                this.SelectedTelerikTemplate = null;
                this.SelectedTemplate = null;
                this.ReportYear = DateTime.Now.Year;
            }

            SiteMaster.StandardMaster.InfoText = "Report";
        }

        private void bindData() {
            this.rgReport.DataSource = this.SiteMaster.ReportDataSource;
            this.rgReport.DataBind();

            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmYear = cmdItem.FindControl("rtbYear") as RadNumericTextBox;
            nmYear.Value = this.ReportYear;

            RadComboBox rlbExcelTemplate = cmdItem.FindControl("rcbExcelTemplate") as RadComboBox;
            rlbExcelTemplate.DataSource = ExcelExporter.GetTemplateFiles(Server);
            rlbExcelTemplate.DataBind();
            rlbExcelTemplate.Items.Where(i => i.Text == this.SelectedTemplate).SingleOrDefault().Selected = true;

            RadComboBox rlbTelerikTemplate = cmdItem.FindControl("rcbTelerikReport") as RadComboBox;
            rlbTelerikTemplate.DataSource = Constants.TELERIK_REPORT_TEMPLATES;
            rlbTelerikTemplate.DataBind();
            rlbTelerikTemplate.Items.Where(i => i.Text == this.SelectedTelerikTemplate).SingleOrDefault().Selected = true;
        }

        
        #region Events

        protected void rgReport_PreRender(object sender, EventArgs e) {
            bindData();
            //fixGridScroll();
        }

        private void fixGridScroll() {
            rgReport.Width = Unit.Percentage(100);
            rgReport.ClientSettings.Scrolling.AllowScroll = true;
            rgReport.ClientSettings.Scrolling.ScrollHeight = 250;
            rgReport.MasterTableView.Width = Unit.Percentage(120);
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e) {
            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(this.rgReport, this.SiteMaster.ReportDataSource, true);
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx?template=" + this.SelectedTemplate /* + "&reportType=Categorized&categorization=Group" */);
        }

        protected void rgReport_Init(object sender, EventArgs e) {
            ArticleGridHelper.ClearFilter(this.rgReport);
        }

        protected void rgReport_DataBound(object sender, EventArgs e) {
            if (rgReport.MasterTableView.GetItems(GridItemType.Footer) != null && rgReport.MasterTableView.GetItems(GridItemType.Footer).Count() > 0) {
                GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;

                List<Article> ReportItems = ArticleGridHelper.GetReportItems(this.rgReport, this.SiteMaster.ReportDataSource, false);

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

        protected void btnApplyYear_Click(object sender, EventArgs e) {
            this.SiteMaster.ReportDataSource.ForEach(a => a.DepreciationTime = new DateTime(this.ReportYear, 1, 1));
            rgReport.Rebind();
        }


        protected void rtbYear_TextChanged(object sender, EventArgs e) {
            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmYear = cmdItem.FindControl("rtbYear") as RadNumericTextBox;
            this.ReportYear = (int)nmYear.Value;
        }

        protected void rcbExcelTemplate_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTemplate = e.Text;
        }

        protected void btnTelerikExport_Click(object sender, EventArgs e) {
            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(this.rgReport, this.SiteMaster.ReportDataSource, false);
            Response.Redirect("~/Site/Administrator/Report/PrintView.aspx?template=" + this.SelectedTelerikTemplate + "&year=" + this.ReportYear + "&title=" + this.ReportTitle);
        }

        protected void rcbTelerikReport_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTelerikTemplate = e.Text;
        }

        #endregion  

    }
}