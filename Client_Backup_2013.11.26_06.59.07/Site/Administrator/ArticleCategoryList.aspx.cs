﻿using Client.SiteMaster;
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

namespace Client.Site.Administrator {
    public partial class ArticleCategoryList : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Artikelkategorie - Verwaltung";
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        protected void rgCategories_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e) {
            if (e.CommandName.ToLower() == "initinsert") {
                Response.Redirect("~/Site/Administrator/ManageArticleCategory.aspx");
            } else if (e.CommandName.ToLower() == "edit") {
                Response.Redirect("~/Site/Administrator/ManageArticleCategory.aspx?ic=" + (e.Item as GridDataItem)["ArticleCategoryId"].Text);
            }
            else if (e.CommandName == "Delete") {
                if (e.Item is GridDataItem) {
                    GridDataItem dataItem = e.Item as GridDataItem;

                    ArticleCategory categoryToDelete = ArticleCategory.GetById(int.Parse(dataItem.GetDataKeyValue("ArticleCategoryId").ToString()));
                    if (categoryToDelete.ArticleGroups.Any()) {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden da Artikel damit versehen sind", categoryToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        categoryToDelete.Delete();
                    }
                }
            } 
        }
    }
}