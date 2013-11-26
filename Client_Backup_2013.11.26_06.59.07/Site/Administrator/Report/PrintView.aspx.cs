using Client.SiteMaster;
using Data.Enum;
using Reporting.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.Site.Administrator.Report {
    public partial class PrintView : System.Web.UI.Page {

        #region Properties

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e) {

            if (Request.Params["template"] != null) {

                TelerikReports selectedReport = Enum.GetValues(typeof(TelerikReports))
                    .Cast<TelerikReports>().ToList().Where(a => a.ToString() == Request.Params["template"]).FirstOrDefault();

                String year = Request.Params["year"];
                String title = Request.Params["title"];

                switch (selectedReport) {
                    case TelerikReports.Standart:
                        this.reportView.Report = new Standard(this.SiteMaster.ExportItems, title, year);
                        this.reportView.DataBind();
                        break;

                    case TelerikReports.RaumCheckliste:
                        this.reportView.Report = new RoomCheckList(this.SiteMaster.ExportItems);
                        this.reportView.DataBind();
                        break;

                    case TelerikReports.RaumGruppierung:
                        this.reportView.Report = new RoomGroupList(this.SiteMaster.ExportItems, title, year);
                        this.reportView.DataBind();
                        break;

                    case TelerikReports.KategorieGruppierung:
                        this.reportView.Report = new KategorieGroupList(this.SiteMaster.ExportItems, title, year);
                        this.reportView.DataBind();

                        break;
                }
            }
        }
    }
}