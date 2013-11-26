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
    public partial class ManageInsuranceCategory : System.Web.UI.Page {

        private InsuranceCategory insuranceCategory {
            get {
                if (Session["InsuranceCategory"] != null) {
                    return Session["InsuranceCategory"] as InsuranceCategory;
                }
                return null;
            }
            set {
                Session["InsuranceCategory"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }
            if (!IsPostBack) {
                this.insuranceCategory = null;
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
            this.insuranceCategory = null;
            if (Request.QueryString["ic"] != null && Request.QueryString["ic"] != "") {
                int categoryId = int.Parse(Request.QueryString["ic"]);
                this.insuranceCategory = InsuranceCategory.GetById(categoryId);
            }
        }

        private void bindData() {
            if (this.insuranceCategory != null) {
                this.rtbName.Text = this.insuranceCategory.Name;
                this.chbIsDefault.Checked = this.insuranceCategory.IsDefault.HasValue ? this.insuranceCategory.IsDefault.Value : false;
            } 
        }

        #endregion


        private void save() {
            if (this.insuranceCategory == null) {
                this.insuranceCategory = new InsuranceCategory();
                EntityFactory.Context.InsuranceCategories.Add(this.insuranceCategory);
            }

            //Set the other categories to non default if this is default
            if (this.chbIsDefault.Checked) {
                InsuranceCategory.GetAll().ToList().ForEach(i => i.IsDefault = false);
            }

            this.insuranceCategory.Name = this.rtbName.Text;
            this.insuranceCategory.IsDefault = this.chbIsDefault.Checked;

            EntityFactory.Context.SaveChanges();
        }

        #region Events

        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("~/Site/Administrator/InsuranceCategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            Response.Redirect("~/Site/Administrator/InsuranceCategoryList.aspx");
        }

        #endregion
    }
}