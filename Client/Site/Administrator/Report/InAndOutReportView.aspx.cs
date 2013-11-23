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
using System.Globalization;
using Client.Site.Administrator.Report;

namespace Client.Site.Administrator {
    public partial class InAndOutReportView : ReportGridBase {

        #region Events

        protected void rgReport_PreRender(object sender, EventArgs e) {
            this.bindData();
            base.fixGridScroll(rgReport);
        }

        private void bindData() {
            base.bindData(this.rgReport);

            GridItem cmdItem = rgReport.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            RadNumericTextBox nmInOutYear = cmdItem.FindControl("rtbInOutYear") as RadNumericTextBox;
            nmInOutYear.Value = this.InOutYear;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e) {
            base.ExportToExcel(this.rgReport);
        }

        protected void rgReport_Init(object sender, EventArgs e) {
            ArticleGridHelper.ClearFilter(this.rgReport);
        }

        protected void rgReport_DataBound(object sender, EventArgs e) {
            if (rgReport.MasterTableView.GetItems(GridItemType.Footer) != null && rgReport.MasterTableView.GetItems(GridItemType.Footer).Count() > 0) {
                GridFooterItem footerItem = rgReport.MasterTableView.GetItems(GridItemType.Footer).ElementAt(0) as GridFooterItem;

                List<Article> ReportItems = ArticleGridHelper.GetReportItems(this.rgReport, this.SiteMaster.ReportDataSource, false);
                double? total = this.ReportType == "in" ? ReportItems.Where(a => a.AcquisitionDate.Value.Year == this.InOutYear).Sum(i => i.Value) : ReportItems.Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == this.InOutYear)).Sum(i => i.Value);
                double? depTotal = this.ReportType == "in" ? ReportItems.Where(a => a.AcquisitionDate.Value.Year == this.InOutYear).Sum(i => i.DepreciationValue) : ReportItems.Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == this.InOutYear)).Sum(i => i.DepreciationValue);
                double? avDepTotal = this.ReportType == "in" ? ReportItems.Where(a => a.AcquisitionDate.Value.Year == this.InOutYear).Sum(i => i.AverageDepreciation) : ReportItems.Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == this.InOutYear)).Sum(i => i.AverageDepreciation);
                double? cumDepTotal = this.ReportType == "in" ? ReportItems.Where(a => a.AcquisitionDate.Value.Year == this.InOutYear).Sum(i => i.CumulatedDepreciation) : ReportItems.Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == this.InOutYear)).Sum(i => i.CumulatedDepreciation);

                footerItem["Value"].Text = Math.Round(total.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["DepreciationValue"].Text = Math.Round(depTotal.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["AverageDepreciation"].Text = Math.Round(avDepTotal.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["CumulatedDepreciation"].Text = Math.Round(cumDepTotal.Value, 2).ToString(Constants.NUMBER_FORMAT, Constants.NUMBER_GROUP_FORMAT);
                footerItem["Name"].Text = "Total:";
            }
        }

        protected void btnApplyYear_Click(object sender, EventArgs e) {
            base.ReCalculateDepreciation(this.rgReport);
        }


        protected void rtbYear_TextChanged(object sender, EventArgs e) {
            base.UpdateReportYear(this.rgReport);
        }

        protected void rcbExcelTemplate_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTemplate = e.Text;
        }

        protected void rtbInOutYear_TextChanged(object sender, EventArgs e) {
            base.UpdateInOutYear(this.rgReport);
        }

        protected void rbtInOutFilter_Click(object sender, EventArgs e) {
            //TODO CHECK
            this.SiteMaster.ReportDataSource.ForEach(a => a.DepreciationTime = new DateTime(this.ReportYear, 1, 1));
            rgReport.Rebind();
        }

        protected void btnTelerikExport_Click(object sender, EventArgs e) {
            base.ExportToPrintView(this.rgReport);
        }

        protected void rcbTelerikReport_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTelerikTemplate = e.Text;
        }

        protected void btnGroupSource_Click(object sender, EventArgs e) {
            base.ToggleGroupSource(this.rgReport);
        }

        #endregion  
    }
}