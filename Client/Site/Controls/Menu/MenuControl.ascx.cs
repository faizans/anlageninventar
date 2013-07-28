using Client.SiteMaster;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Controls.Menu {
    public partial class MenuControl : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
           
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #region Events

        protected void lbMenuItemClicked(object sender, EventArgs e) {
            LinkButton menuItem = (LinkButton)sender;
            switch (menuItem.CommandName) {
                case "DeletedArticles":
                    this.SiteMaster.ReportDataSource = Article.GetDeleted().ToList();
                    break;
                case "AllArticles":
                    this.SiteMaster.ReportDataSource = Article.GetAvailable().ToList();
                    break;
            }
            Response.Redirect("~/Site/Administrator/ReportView.aspx");
        }

        #endregion
    }
}