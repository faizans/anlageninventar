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
            if (!this.rgReport.MasterTableView.FilterExpression.Any()) {
                reportSource = Article.GetAvailable().ToList();
            } else {
                //Get the filter expression
                String filter = this.rgReport.MasterTableView.FilterExpression;

                //HAndle speical chars
                filter = filter.Replace("[", "");
                filter = filter.Replace("]", "");
                filter = filter.Replace("(", "");
                filter = filter.Replace(")", "");
                filter = filter.Replace("%", "");
                filter = filter.Replace("'", "\"");

                //Handle expressions
                if (filter.Contains("AND")) {
                    foreach (String queryPart in filter.Split("AND".ToCharArray())) {
                        filter = handleArguments(filter, queryPart);
                    }
                } else {
                    filter = handleArguments(filter, filter);
                }

                reportSource = Article.GetAvailable().AsQueryable().Where(filter).ToList();

                if (rgReport.MasterTableView.GetItems(GridItemType.TFoot).Any()) {
                    GridFooterItem dataItem = rgReport.MasterTableView.GetItems(GridItemType.Footer)[0] as GridFooterItem;
                    Article article = new Article();
                    article.Name = "Total:";
                    article.Value = double.Parse(dataItem["Value"].Text);
                    reportSource.Add(article);
                }
            }
            return reportSource;
        }

        private String handleArguments(String filter, String queryPart) {
            if (queryPart.Contains("LIKE")) {
                String[] qparams = queryPart.Split("LIKE".ToCharArray());
                filter = filter.Replace(queryPart, " " + qparams[0] + ".Contains(" + qparams[4] + ")");
            }
            return filter;
        }

        #region Events

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e) {
            this.SiteMaster.ExportItems = GetReportItems();
            this.Context.Session["ExportItems"] = GetReportItems();
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx");
        }

        protected void rgReport_Init(object sender, EventArgs e) {
            GridFilterMenu menu = rgReport.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count) {
                if (menu.Items[i].Text == "NotContains" || menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith" || menu.Items[i].Text == "Between"
                 || menu.Items[i].Text == "NotBetween" || menu.Items[i].Text == "IsEmpty" || menu.Items[i].Text == "NotIsEmpty" || menu.Items[i].Text == "IsNull" || menu.Items[i].Text == "NotIsNull") {
                    menu.Items.RemoveAt(i);
                }
                i++;
            }
        }

        protected void rgReport_DataBound(object sender, EventArgs e) {
            GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;
            double? total = this.rgReport.MasterTableView.GetItems(GridItemType.Item, GridItemType.AlternatingItem).Sum(i => (i.DataItem as Article).Value);
            footerItem["Value"].Text = total.ToString();
            footerItem["Name"].Text = "Total:";
        }

        #endregion
    }
}