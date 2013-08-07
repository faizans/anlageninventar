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
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
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
            rlbExcelTemplate.Items.Where(i =>i.Text == this.SelectedTemplate).SingleOrDefault().Selected=true;
        }

        
        #region Events

        protected void rgReport_PreRender(object sender, EventArgs e) {
            bindData();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e) {
            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(this.rgReport, this.SiteMaster.ReportDataSource, true);
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx?template="+this.SelectedTemplate);
        }

        protected void rgReport_Init(object sender, EventArgs e) {
            ArticleGridHelper.ClearFilter(this.rgReport);
        }

        protected void rgReport_DataBound(object sender, EventArgs e) {
            if (rgReport.MasterTableView.GetItems(GridItemType.Footer) != null && rgReport.MasterTableView.GetItems(GridItemType.Footer).Count() > 0) {
                GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;
                double? total = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).Value);
                double? depTotal = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).DepreciationValue);

                footerItem["Value"].Text = Math.Round(total.Value, 2).ToString();
                footerItem["DepreciationValue"].Text = Math.Round(depTotal.Value, 2).ToString();
                footerItem["Name"].Text = "Total:";
            }
        }

        protected void btnApplyYear_Click(object sender, EventArgs e) {
            this.SiteMaster.ReportDataSource.ForEach(a => a.DepreciationTime = new DateTime(this.ReportYear, 1, 1));
            this.rgReport.Rebind();
        }


        protected void rtbYear_TextChanged(object sender, EventArgs e) {
            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmYear = cmdItem.FindControl("rtbYear") as RadNumericTextBox;
            this.ReportYear = (int)nmYear.Value;
        }

        protected void rcbExcelTemplate_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTemplate = e.Text;
        }

        #endregion  
    }
}