
using Client.Site.Controls.RoomTree;
using Client.SiteMaster;
using Client.Util;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Administrator
{
    public partial class RoomList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Lagerräume - Verwaltung";
            if (!IsPostBack) {
                this.RoomTreeControl.RoomTreeItems = null;
            }
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

    }
}