using Client.SiteMaster;
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
    public partial class ArticleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMaster.StandardMaster.InfoText = "Artikel - Verwaltung";
            //if (!IsPostBack) {
            //    bindData();
            //}
            bindData();
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        private void bindData() {
            this.rgArticles.DataSource = Article.GetAll().ToList();
            this.rgArticles.DataBind();
        }

        protected void rgArticles_ItemCommand(object sender, GridCommandEventArgs e) {
            if (e.CommandName.ToLower() == "initinsert") {
                Response.Redirect("~/Site/Administrator/ManageArticle.aspx");
            } else if (e.CommandName.ToLower() == "edit") {
                Response.Redirect("~/Site/Administrator/ManageArticle.aspx?ai=" + (e.Item as GridDataItem)["ArticleId"].Text);
            } else if (e.CommandName.ToLower() == "delete") {
                Article articleToDelete = Article.GetById(int.Parse((e.Item as GridDataItem)["ArticleId"].Text));
                if (articleToDelete != null) {
                    if (articleToDelete.IsDepreciated()) {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da der Artikel noch nicht Abgeschrieben wurde.", articleToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        articleToDelete.Delete();
                        EntityFactory.Context.SaveChanges();
                        bindData();
                    }
                }
            }
        }

    }
}