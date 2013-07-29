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

namespace Client.Site.Administrator {
    public partial class ReportView : System.Web.UI.Page {


        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Report";
            bindData();
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        private void bindData() {
            this.rgReport.DataSource = null; ;
            this.rgReport.DataSource = this.SiteMaster.ReportDataSource;
            this.rgReport.DataBind();
        }

        
        #region Events

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e) {
            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(this.rgReport, this.SiteMaster.ReportDataSource, true);
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx");
        }

        protected void rgReport_Init(object sender, EventArgs e) {
            ArticleGridHelper.ClearFilter(this.rgReport);
        }

        protected void rgReport_DataBound(object sender, EventArgs e) {
            GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;
            double? total = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).Value);
            double? depTotal = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).DepreciationValue);

            footerItem["Value"].Text = Math.Round(total.Value, 2).ToString();
            footerItem["DepreciationValue"].Text = Math.Round(depTotal.Value, 2).ToString();
            footerItem["Name"].Text = "Total:";
        }

        #endregion
    }
}