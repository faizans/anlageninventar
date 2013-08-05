using Client.SiteMaster;
using Client.Util;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client {
    public partial class _Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            AppUser user = AppUser.GetByManualLogin(this.rtbUsername.Text, this.rtbPassword.Text);
            if (user == null) {
                this.lblAlert.Text = "Login fehlgeschlagen. Bitte Benutzernamen und Password eingeben.";
            } else {
                this.SiteMaster.User = user;
                Global.SetUpFormAuthenticationTicket(user.UserName, user.IsAdmin ? "Administrator" : "User", Response);

                Response.Redirect(Constants.DEFAULT_PAGE);
            }
        }
    }
}