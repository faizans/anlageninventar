using Client.SiteMaster;
using Client.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Administrator {
    public partial class UploadTemplate : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }
            if (!IsPostBack)
                bindData();
        }

        public RadListBoxItem SelectedItem {
            get {
                if (Session["SelectedTemplateItem"] != null) {
                    return Session["SelectedTemplateItem"] as RadListBoxItem;
                }
                return null;
            }
            set {
                Session["SelectedTemplateItem"] = value;
            }
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        private void bindData() {
            this.rlbTemplates.DataSource = ExcelExporter.GetTemplateFiles(Server) ;
            this.rlbTemplates.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e) {
            foreach (UploadedFile f in rauExcelTemplate.UploadedFiles) {
                f.SaveAs(Path.Combine(Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER), f.GetName()));
            }
            bindData();
        }

        protected void rlbTemplates_Deleting(object sender, RadListBoxDeletingEventArgs e) {
            if (this.rlbTemplates.SelectedItem != null) {
                FileInfo fileToDelete = new FileInfo(this.rlbTemplates.SelectedItem.Value);
                fileToDelete.Delete();
            }
        }
    }
}