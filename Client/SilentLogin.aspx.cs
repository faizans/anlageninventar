using Client.SiteMaster;
using Client.Util;
using Data.Enum;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client {
    public partial class _SilentLogin : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        protected override void OnPreRender(EventArgs e) {

            string userid = Request.ServerVariables["LOGON_USER"];

            if (userid != null && userid.Length > 0) {

                AppUser loggedUser = AppUser.GetByLogin(Request.LogonUserIdentity, userid);

                if (loggedUser != null && loggedUser.IsActive && loggedUser.IsAdmin) {
                    //Global.SetUpFormAuthenticationTicket(loggedUser, "Administrator", Response);

                    Session[SessionName.LoggedUser.ToString()] = loggedUser;

                    Response.Redirect(Constants.DEFAULT_PAGE);
                } else {
                    Response.Redirect(Constants.AUTHORIZATION_FORMS_LOGIN);
                }
            } else {
                Response.Redirect(Constants.AUTHORIZATION_FORMS_LOGIN);
            }
        }
    }
}