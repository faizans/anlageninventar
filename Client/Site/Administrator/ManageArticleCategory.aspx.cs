using Client.Site.Controls.ListBox2;
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

namespace Client.Site.Administrator {
    public partial class ManageArticleCategory : System.Web.UI.Page {

        private ArticleCategory articleCategory {
            get {
                if (Session["ArticleCategory"] != null) {
                    return Session["ArticleCategory"] as ArticleCategory;
                }
                return null;
            }
            set {
                Session["ArticleCategory"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }
            if (!IsPostBack) {
                loadPage();
            }
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }


        #region Initialisation

        private void loadPage() {
            if (!IsPostBack) {
                getParameters();
                bindData();
            }
        }

        private void getParameters() {
            this.articleCategory = null;
            if (Request.QueryString["ic"] != null && Request.QueryString["ic"] != "") {
                int categoryId = int.Parse(Request.QueryString["ic"]);
                this.articleCategory = ArticleCategory.GetById(categoryId);
            }
        }

        private void bindData() {
            if (this.articleCategory != null) {
                this.rtbName.Text = this.articleCategory.Name;
            } 
        }

        #endregion


        private void save() {
            if (this.articleCategory == null) {
                this.articleCategory = new ArticleCategory();
                EntityFactory.Context.ArticleCategories.Add(this.articleCategory);
            }

            this.articleCategory.Name = this.rtbName.Text;

            EntityFactory.Context.SaveChanges();
        }

        #region Events

        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("~/Site/Administrator/ArticleCategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            Response.Redirect("~/Site/Administrator/ArticleCategoryList.aspx");
        }

        #endregion
    }
}