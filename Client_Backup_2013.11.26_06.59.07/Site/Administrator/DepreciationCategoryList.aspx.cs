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
    public partial class DepreciationCategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Abschreibungskategorien - Verwaltung";
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        private void bindData() {
            this.rgCategories.DataSource = DepreciationCategory.GetAll().ToList();
            this.rgCategories.DataBind();
        }

        protected void rgCategories_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "initinsert")
            {
                Response.Redirect("~/Site/Administrator/ManageDepreciationCategory.aspx");
            }
            else if (e.CommandName.ToLower() == "edit")
            {
                Response.Redirect("~/Site/Administrator/ManageDepreciationCategory.aspx?ci=" + (e.Item as GridDataItem)["DepreciationCategoryId"].Text);
            }
            else if (e.CommandName.ToLower() == "delete")
            {
                DepreciationCategory depreciationCategoryToDelete = DepreciationCategory.GetById(int.Parse((e.Item as GridDataItem)["DepreciationCategoryId"].Text));
                if (depreciationCategoryToDelete != null) {
                    if (depreciationCategoryToDelete.HasArticles()) {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Kategorie mit Artikeln verknüpft ist.", depreciationCategoryToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        depreciationCategoryToDelete.Delete();
                        EntityFactory.Context.SaveChanges();
                        bindData();
                    }
                }
            } 
        }

        protected void rgCategories_PreRender(object sender, EventArgs e) {
            bindData();
        }

    }
}