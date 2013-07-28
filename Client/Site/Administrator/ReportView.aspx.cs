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

namespace Client.Site.Administrator {
    public partial class ReportView : System.Web.UI.Page {


        protected void Page_Load(object sender, EventArgs e) {
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

        protected void rgArticles_ItemCommand(object sender, GridCommandEventArgs e) {

        }

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e) {

        }

        #endregion

        protected void rgReport_DataBound(object sender, EventArgs e) {
            GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;
            double? total = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).Value);
            footerItem["Value"].Text = total.ToString();
            footerItem["Name"].Text = "Total:";
        }

        /*
        * FILTER IDEA
        * DEFINE TABLE IN DATABASE IN WICH YOU CAN SAY WHICH FIELDS SHOULD BE FILTERED BY WHICH DATA
        * ON MENUITEM CLICK LOAD THE FIELDVALUE AND FILTER DATASOURCE BY THIS FIELDVALUE
        * OVERGIVE DATASOURCE TO REPORTVIEW AND DISABLE FILTERING
        */

    }
}