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
            }
        }
    }
}