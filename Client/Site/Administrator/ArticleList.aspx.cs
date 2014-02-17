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
using Data.Enum;

namespace Client.Site.Administrator {
    public partial class ArticleList : System.Web.UI.Page {

        #region Properties

        private Room selectedTargetRoom = null;

        private String SelectedTemplate {
            get {
                if (Session[SessionName.SelectedTemplate.ToString()] == null) {
                    Session[SessionName.SelectedTemplate.ToString()] = ExcelExporter.GetTemplateFiles(Server)[0].Name;
                }
                return Session[SessionName.SelectedTemplate.ToString()].ToString();
            }
            set {
                Session[SessionName.SelectedTemplate.ToString()] = value;
            }
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e) {

            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            SiteMaster.StandardMaster.InfoText = "Artikel - Verwaltung";
        }

        private void bindData() {
            GridItem cmdItem = rgArticles.MasterTableView.GetItems(GridItemType.CommandItem)[0];
            //Bind the data for the excel template combobox
            RadComboBox rlbExcelTemplate = cmdItem.FindControl("rcbExcelTemplate") as RadComboBox;
            rlbExcelTemplate.DataSource = ExcelExporter.GetTemplateFiles(Server);
            rlbExcelTemplate.DataBind();
            rlbExcelTemplate.Items.Where(i => i.Text == this.SelectedTemplate).SingleOrDefault().Selected = true;
        }

        #region Events

        protected void rgArticles_ItemCommand(object sender, GridCommandEventArgs e) {
            if (e.CommandName.ToLower() == "initinsert") {
                Response.Redirect("~/Site/Administrator/ManageArticle.aspx");
            } else if (e.CommandName.ToLower() == "edit") {
                Response.Redirect("~/Site/Administrator/ManageArticle.aspx?ai=" + (e.Item as GridDataItem)["ArticleId"].Text);
            } else if (e.CommandName.ToLower() == "moveselection" && this.selectedTargetRoom != null) {
                foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                    if ((dataItem.FindControl("chbSelection") as CheckBox).Checked) {
                        if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                            Article articleToMove = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                            if (articleToMove != null) {
                                articleToMove.Room = this.selectedTargetRoom;
                            }
                            if (articleToMove.ArticleGroup != null
                                && !articleToMove.ArticleGroup.Articles.Where(a => !a.IsDeleted && a.Room.Name != this.selectedTargetRoom.Name).Any()) {
                                articleToMove.ArticleGroup.Room = this.selectedTargetRoom;
                            }
                        }
                    }
                }
                EntityFactory.Context.SaveChanges();
                rgArticles.Rebind();
                RadWindowManager1.RadAlert("Auswahl wurde verschoben", null, 100, "Artikel verschieben", "alertCallBackFn");

            }else if (e.CommandName.ToLower() == "deleteselection") {
                foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                    if ((dataItem.FindControl("chbSelection") as CheckBox).Checked) {
                        if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                            Article articleToDelete = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                            if (articleToDelete != null) {
                                articleToDelete.Delete();
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

            bindData();
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

        protected void btnExportToExcel_Click(object sender, EventArgs e) {
            this.SiteMaster.ExportItems = ArticleGridHelper.GetReportItems(this.rgArticles, Article.GetAvailable().ToList(),false);
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx?template=" + this.SelectedTemplate);
        }

        protected void rgArticles_Init(object sender, EventArgs e) {
            ArticleGridHelper.ClearFilter(this.rgArticles);
        }

        protected void rcbExcelTemplate_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            this.SelectedTemplate = e.Text;
        }

        protected void btnReport_Click(object sender, EventArgs e) {
            this.SiteMaster.ReportDataSource = ArticleGridHelper.GetReportItems(this.rgArticles, Article.GetAvailable().ToList(), false);
            this.SiteMaster.UngroupedReportDataSource = new List<Article>(this.SiteMaster.ReportDataSource);
            Response.Redirect("~/Site/Administrator/Report/ReportView.aspx");
        }

        protected void btnReportView_Click(object sender, EventArgs e) {
            this.SiteMaster.ReportDataSource = ArticleGridHelper.GetReportItems(this.rgArticles, Article.GetAvailable().ToList(), false);
            this.SiteMaster.UngroupedReportDataSource = new List<Article>(this.SiteMaster.ReportDataSource);
            Response.Redirect("~/Site/Administrator/Report/ReportView.aspx");
        }

        #endregion


    }
}