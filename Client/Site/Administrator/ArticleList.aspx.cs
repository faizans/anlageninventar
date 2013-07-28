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

namespace Client.Site.Administrator {
    public partial class ArticleList : System.Web.UI.Page {

        private Room selectedTargetRoom = null;

        protected void Page_Load(object sender, EventArgs e) {
            SiteMaster.StandardMaster.InfoText = "Artikel - Verwaltung";
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        private List<Article> GetReportItems() {
            List<Article> reportSource = new List<Article>();
            foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                    Article article = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                    reportSource.Add(article);
                } else if (dataItem.ItemType == GridItemType.Footer) {
                    Article article = new Article();
                    article.Name = "Total:";
                    article.Value = double.Parse(dataItem["Value"].Text);
                    reportSource.Add(article);
                }
            }
            return reportSource;
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

            } else if (e.CommandName.ToLower() == "delete") {
                deleteArticle(int.Parse((e.Item as GridDataItem)["ArticleId"].Text));
            } else if (e.CommandName.ToLower() == "deleteselection") {
                foreach (GridDataItem dataItem in rgArticles.MasterTableView.Items) {
                    if ((dataItem.FindControl("chbSelection") as CheckBox).Checked) {
                        if (dataItem.ItemType == GridItemType.Item || dataItem.ItemType == GridItemType.AlternatingItem) {
                            Article articleToDelete = Article.GetById(int.Parse(dataItem["ArticleId"].Text));
                            if (articleToDelete != null) {
                                if (articleToDelete.IsDepreciated()) {
                                    articleToDelete.Delete();
                                } else {
                                    //TODO Generate alert which tells that not all were deleted
                                }
                            }
                        }
                    }
                }
                EntityFactory.Context.SaveChanges();
                rgArticles.Rebind();
            }
        }

        private void deleteArticle(int id) {
            Article articleToDelete = Article.GetById(id);
            if (articleToDelete != null) {
                if (!articleToDelete.IsDepreciated()) {
                    RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da der Artikel noch nicht Abgeschrieben wurde.", articleToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                } else {
                    articleToDelete.Delete();
                    EntityFactory.Context.SaveChanges();
                }
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

        protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e) {
            //ConfigureExport();
            //rgArticles.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            //rgArticles.MasterTableView.ExportToExcel();

            this.SiteMaster.ExportItems = GetReportItems();
            this.Context.Session["ExportItems"] = GetReportItems();
            Response.Redirect("~/Site/Provider/ExcelProvider.ashx");
        }

        #endregion

        #region ExportSettings
        public void ConfigureExport() {
            rgArticles.ExportSettings.ExportOnlyData = true;
            rgArticles.ExportSettings.IgnorePaging = true;
            rgArticles.ExportSettings.OpenInNewWindow = false;
            rgArticles.ExportSettings.UseItemStyles = false;
        }

        protected void btnReport_Click(object sender, ImageClickEventArgs e) {
            this.SiteMaster.ReportDataSource = GetReportItems() ;
            Response.Redirect("~/Site/Administrator/ReportView.aspx");
        }
        #endregion

    }
}