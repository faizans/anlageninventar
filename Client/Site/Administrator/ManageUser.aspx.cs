using Client.Site.Controls.UserSearchControl;
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

namespace Client.Site.Administrator
{
    public partial class ManageUser : System.Web.UI.Page
    {

        private AppUser appUser;
        private bool isAdAccount = false;

        protected void Page_Load(object sender, EventArgs e) {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            }

            loadPage();
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #region Initialisation

        private void loadPage()
        {
            getParameters();
            if (!IsPostBack)
            {
                bindSupplierData(this.appUser);
            }
        }

        private void getParameters()
        {
            if (Request.QueryString["ui"] != null && Request.QueryString["ui"] != "")
            {
                int appUserId = int.Parse(Request.QueryString["ui"]);
                this.appUser = AppUser.GetById(appUserId);
                if (appUser != null)
                {
                    this.UserSearchBox.Enabled = false;
                    this.isAdAccount = appUser.IsAdAccount;
                }
            }
        }

        private void bindSupplierData(AppUser appUser)
        {
            if (appUser != null)
            {
                this.rtbFirstNAme.Text = appUser.FirstName;
                this.rtbLastName.Text = appUser.LastName;
                this.rtbDomain.Text = appUser.Domain;
                this.rtbUsername.Text = appUser.UserName;
                this.rtbEmail.Text = appUser.Email;
                this.rtbPasswort.Text = appUser.Password;
                this.chbIsActive.Checked = appUser.IsActive;
                this.chbIsAdmin.Checked = appUser.IsAdmin;

                if (this.isAdAccount)
                {
                    this.rtbFirstNAme.Enabled = false;
                    this.rtbLastName.Enabled = false;
                    this.rtbUsername.Enabled = false;
                    this.rtbEmail.Enabled = false;
                }
            }
        }

        private void save()
        {
            if (this.appUser == null)
            {
                this.appUser = new AppUser();
                this.appUser.IsAdAccount = this.isAdAccount;
                EntityFactory.Context.AppUsers.Add(this.appUser);
            }

            this.appUser.FirstName = this.rtbFirstNAme.Text;
            this.appUser.LastName = this.rtbLastName.Text;
            this.appUser.Domain = this.rtbDomain.Text;
            this.appUser.UserName = this.rtbUsername.Text;
            this.appUser.Email = this.rtbEmail.Text;
            this.appUser.Password = this.rtbPasswort.Text;
            this.appUser.IsActive = this.chbIsActive.Checked;
            this.appUser.IsAdmin = this.chbIsAdmin.Checked;

            EntityFactory.Context.SaveChanges();
            Response.Redirect("~/Site/Administrator/UserList.aspx");
        }

        #endregion

        #region Events

        protected void UserSearchBox_UserSearchBoxIndexChanged(object sender, EventArgs e)
        {
            this.isAdAccount = false;

            UserSearchBoxEventArgs userEvent = e as UserSearchBoxEventArgs;

            if (userEvent != null && userEvent.SelectedUser != null)
            {
                this.isAdAccount = true;
                bindSupplierData(userEvent.SelectedUser);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Site/Administrator/UserList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        #endregion
    }
}