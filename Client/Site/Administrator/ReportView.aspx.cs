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

        private List<Article> GetReportItems() {
            List<Article> reportSource = new List<Article>();
            foreach (GridDataItem dataItem in rgReport.MasterTableView.Items) {
                if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                    Article article = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                    reportSource.Add(article);
                } 
            }

            GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;
            Article fakeArticle = new Article();
            fakeArticle.Name = "Total:";
            fakeArticle.Value= double.Parse(footerItem["Value"].Text);

            reportSource.Add(fakeArticle);

            return reportSource;
        }

        #region Events

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e) {
            this.SiteMaster.ExportItems = GetReportItems();
            this.Context.Session["ExportItems"] = GetReportItems();
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx");
        }

        #endregion

        protected void rgReport_DataBound(object sender, EventArgs e) {
            GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;
            double? total = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).Value);
            footerItem["Value"].Text = total.ToString();
            footerItem["Name"].Text = "Total:";
        }
    }
}