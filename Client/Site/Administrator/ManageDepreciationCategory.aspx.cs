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
    public partial class ManageDepreciationCategory : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            loadPage();
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #region Properties

        private DepreciationCategory depreciationCategory {
            get {
                return Session["DepreciationCategory"] as DepreciationCategory;
            }
            set {
                Session["DepreciationCategory"] = value;
            }
        }

        #endregion

        #region Initialisation

        private void loadPage() {
            if (!IsPostBack) {
                getParameters();
                bindData();
            }
        }

        private void getParameters() {
            this.depreciationCategory = null;
            if (Request.QueryString["ci"] != null && Request.QueryString["ci"] != "") {
                int categoryId = int.Parse(Request.QueryString["ci"]);
                this.depreciationCategory = DepreciationCategory.GetById(categoryId);
            }
        }

        private void bindData() {
            if (this.depreciationCategory != null) {
                this.rtbName.Text = this.depreciationCategory.Name;
                this.rtbTimeSpan.Text = this.depreciationCategory.DepreciationSpan.HasValue ? this.depreciationCategory.DepreciationSpan.Value.ToString() : "";
            } 
        }

        #endregion

        private void save() {
            if (this.depreciationCategory == null) {
                this.depreciationCategory = new DepreciationCategory();
                EntityFactory.Context.DepreciationCategories.Add(depreciationCategory);
            }
            this.depreciationCategory.Name = this.rtbName.Text;
            this.depreciationCategory.DepreciationSpan = this.rtbTimeSpan.Value;
            EntityFactory.Context.SaveChanges();
        }

        #region FormEvents

        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("~/Site/Administrator/DepreciationCategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            Response.Redirect("~/Site/Administrator/DepreciationCategoryList.aspx");
        }

        #endregion

    }
}