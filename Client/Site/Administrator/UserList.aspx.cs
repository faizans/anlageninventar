using Client.SiteMaster;
using Client.Util;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Administrator
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Benutzer - Verwaltung";
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        protected void rgUsers_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "initinsert")
            {
                Response.Redirect("~/Site/Administrator/ManageUser.aspx");
            }
            else if (e.CommandName.ToLower() == "edit")
            {
                Response.Redirect("~/Site/Administrator/ManageUser.aspx?ui=" + (e.Item as GridDataItem)["AppUserId"].Text);
            }
            else if (e.CommandName.ToLower() == "delete")
            {
                AppUser userToDelete = AppUser.GetById(int.Parse((e.Item as GridDataItem)["AppUserId"].Text));
                if (userToDelete != null) {
                    if (userToDelete.Rooms.Any()) {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Benutzer für Raum zuständig ist.", userToDelete.Email), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        userToDelete.Delete();
                        EntityFactory.Context.SaveChanges();
                    }
                }
            } else if (e.CommandName.ToLower() == "login") {
                AppUser userToLogin = AppUser.GetById(int.Parse((e.Item as GridDataItem)["AppUserId"].Text));
                this.SiteMaster.User = userToLogin;

                RadWindowManager1.RadAlert(String.Format("Sie agieren nun als {0}", (userToLogin.FirstName + " " + userToLogin.LastName)), 300, 130, "Benutzer wechseln", "alertCallBackFn");
            }
        }
    }
}