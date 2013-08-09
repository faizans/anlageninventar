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
    public partial class SupplierList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Lieferanten - Verwaltung";
            if (!IsPostBack) {
                bindData();
            }
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        private void bindData() {
            this.rgSupplierlist.DataSource = Supplier.GetAll().ToList();
            this.rgSupplierlist.DataBind();
        }

        protected void rgCategories_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "initinsert")
            {
                Response.Redirect("~/Site/Administrator/ManageSupplier.aspx");
            }
            else if (e.CommandName.ToLower() == "edit")
            {
                Response.Redirect("~/Site/Administrator/ManageSupplier.aspx?si=" + (e.Item as GridDataItem)["SupplierId"].Text);
            }
            else if (e.CommandName.ToLower() == "delete")
            {
                Supplier supplierToDelete = Supplier.GetById(int.Parse((e.Item as GridDataItem)["SupplierId"].Text));
                if (supplierToDelete != null) {
                    if (supplierToDelete.HasArticles()) {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Lieferant mit Artikeln verknüpft ist.", supplierToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        supplierToDelete.Delete();
                        EntityFactory.Context.SaveChanges();
                        bindData();
                    }
                }
            }
        }

    }
}