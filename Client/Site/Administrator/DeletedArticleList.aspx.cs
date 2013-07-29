using Client.SiteMaster;
using Client.Util;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq.Dynamic;
using System.Data;
using Client.Site.Controls.CustomGrids;

namespace Client.Site.Administrator {
    public partial class DeletedArticleList : System.Web.UI.Page {

        private Room selectedTargetRoom = null;

        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Gelöschte Artikel - Verwaltung";
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #region Events

        protected void rgArticles_ItemCommand(object sender, GridCommandEventArgs e) {
            if (e.CommandName.ToLower() == "reverseselection") {
                foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                    if ((dataItem.FindControl("chbSelection") as CheckBox).Checked) {
                        if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                            Article articleToReverse = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                            if (articleToReverse != null) {
                                articleToReverse.IsDeleted = false;
                            }
                        }
                    }
                }
                EntityFactory.Context.SaveChanges();
                rgArticles.Rebind();

            } else if (e.CommandName.ToLower() == "reallydelete") {
                foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                    if ((dataItem.FindControl("chbSelection") as CheckBox).Checked) {
                        if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                            Article articleToDelete = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                            if (articleToDelete != null) {
                                articleToDelete.DeletePhysically();
                            }
                        }
                    }
                }
                EntityFactory.Context.SaveChanges();
                rgArticles.Rebind();
            }
        }

        /// <summary>
        /// Select or deslect row item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToggleRowSelection(object sender, EventArgs e) {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
        }

        /// <summary>
        /// Select or deselect header checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToggleSelectedState(object sender, EventArgs e) {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                (dataItem.FindControl("chbSelection") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }

        protected void rgArticles_PreRender(object sender, EventArgs e) {
            if (rgArticles.MasterTableView.GetItems(GridItemType.Header).Any()) {
                GridHeaderItem headerItem = rgArticles.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
                if ((headerItem.FindControl("chbHeaderSelection") as CheckBox) != null) {
                    (headerItem.FindControl("chbHeaderSelection") as CheckBox).Checked = rgArticles.SelectedItems.Count == rgArticles.Items.Count;
                }
            }
        }

        private void rgUploads_ItemPreRender(object sender, EventArgs e) {
            ((sender as GridDataItem)["CheckBoxTemplateColumn"].FindControl("chbSelection") as CheckBox).Checked = (sender as GridDataItem).Selected;
        }

        protected void rgArticles_ItemCreated(object sender, GridItemEventArgs e) {
            if (e.Item is GridDataItem) {
                e.Item.PreRender += new EventHandler(rgArticles_PreRender);
            }
        }

        protected void rcbTargetRoom_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.selectedTargetRoom = Room.GetById(int.Parse(e.Value));
        }

        #endregion

    }
}