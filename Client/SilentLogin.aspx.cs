using Client.Util;
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

        protected override void OnPreRender(EventArgs e) {
            string userid = Request.ServerVariables["LOGON_USER"];
            if (userid != null && userid.Length > 0) {
                String username = AppUser.GetUserNameFromDomainString(userid);
                String domain = AppUser.GetDomainFromDomainString(userid);

                AppUser loggedUser = AppUser.GetByUserNameAndDomain(username, domain);
                if (loggedUser != null && loggedUser.IsActive && loggedUser.IsAdmin) {
                    Global.SetUpFormAuthenticationTicket(userid, "Administrator", Response);
                    Response.Redirect(Constants.DEFAULT_PAGE);
                } else {
                    Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
                }
            } else {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }
        }
    }
}