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

        }

        protected void btnUpload_Click(object sender, EventArgs e) {
            foreach (UploadedFile f in rauExcelTemplate.UploadedFiles) {
                f.SaveAs(Path.Combine(Server.MapPath(Constants.EXCEL_TEMPLATE_FOLDER), Constants.EXCEL_TEMPLATE_NAME));
            }
        }
    }
}