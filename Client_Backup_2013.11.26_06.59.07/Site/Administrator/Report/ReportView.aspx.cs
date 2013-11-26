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
using Client.Site.Administrator.Report;

namespace Client.Site.Administrator {
    public partial class ReportView : ReportGridBase {
        
        #region Events

        protected void rgReport_PreRender(object sender, EventArgs e) {
            base.bindData(this.rgReport);
            base.fixGridScroll(this.rgReport);
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e) {
            base.ExportToExcel(this.rgReport);
        }

        protected void rgReport_Init(object sender, EventArgs e) {
            ArticleGridHelper.ClearFilter(this.rgReport);
        }

        protected void rgReport_DataBound(object sender, EventArgs e) {
            base.GenerateFooter(this.rgReport);
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

        protected void btnTelerikExport_Click(object sender, EventArgs e) {
            base.ExportToPrintView(this.rgReport);
        }

        protected void rcbTelerikReport_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTelerikTemplate = e.Text;
        }

        protected void btnGroupSource_Click(object sender, EventArgs e) {
            this.ToggleGroupSource(this.rgReport);
        }

        #endregion  

    }
}