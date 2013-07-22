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

        protected void Page_Load(object sender, EventArgs e)
        {
            loadPage();
        }

        #region Initialisation

        private void loadPage()
        {
            getParameters();
            if (!IsPostBack)
            {
                bindSupplierData();
            }
        }

        private void getParameters()
        {
            if (Request.QueryString["ui"] != null && Request.QueryString["ui"] != "")
            {
                int appUserId = int.Parse(Request.QueryString["ui"]);
                this.appUser = AppUser.GetById(appUserId);
            }
        }

        private void bindSupplierData()
        {
            if (this.appUser != null)
            {
                this.rtbFirstNAme.Text = this.appUser.FirstName;
                this.rtbLastName.Text = this.appUser.LastName;
                this.rtbDomain.Text = this.appUser.Domain;
                this.rtbUsername.Text = this.appUser.UserName;
                this.rtbEmail.Text = this.appUser.Email;
                this.rtbPasswort.Text = this.appUser.Password;
                this.chbIsActive.Checked = this.appUser.IsActive;
                this.chbIsAdmin.Checked = this.appUser.IsAdmin;
            }
        }

        private void save()
        {
            if (this.appUser == null)
            {
                this.appUser = new AppUser();
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