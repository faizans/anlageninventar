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
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e) {
            foreach (UploadedFile f in rauExcelTemplate.UploadedFiles) {
                f.SaveAs(Path.Combine(Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER), Constants.EXCEL_TEMPLATE_NAME));
            }
        }
    }
}