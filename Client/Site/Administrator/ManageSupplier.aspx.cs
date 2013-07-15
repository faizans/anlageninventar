
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.Site.Administrator
{
    public partial class ManageSupplier : System.Web.UI.Page
    {


        #region Properties

        private Supplier supplier;
        private SupplierBranch selectedBranch;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            loadPage();
        }

        private void loadPage()
        {
            getParameters();
            if (!IsPostBack)
            {
                bindDataSources();
                bindSupplierData();
            }
        }

        private void bindDataSources()
        {
            this.branchList.SelectedIndex = 0;
        }

        private void getParameters()
        {
            if (Request.QueryString["si"] != null)
            {
                int supplierId = int.Parse(Request.QueryString["si"]);
                this.supplier = Supplier.GetById(supplierId);
            }
        }

        private void bindSupplierData()
        {
            if (this.supplier != null)
            {
                this.rtbName.Text = this.supplier.Name;

                if (this.supplier.SupplierBranches.Any())
                {
                    this.branchList.DataSource = this.supplier.SupplierBranches;
                    this.branchList.DataBind();

                    this.selectedBranch = this.supplier.SupplierBranches.ElementAt(0);
                    updateBranch();
                }
            }
        }

        private void updateBranch()
        {
            if (this.selectedBranch != null)
            {
                this.rtbBranchPlace.Text = this.selectedBranch.Place;
                this.rtbBranchPlz.Text = this.selectedBranch.ZipCode;
            }
        }

        private void save() { 
        }

        #region Events

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Site/Administrator/SupplierList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        #endregion

        protected void branchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SupplierBranch selectedItem = SupplierBranch.GetById(int.Parse(this.branchList.SelectedValue));
            if (selectedItem != null)
            {
                this.rtbBranchPlace.Text = selectedItem.Place;
                this.rtbBranchPlz.Text = selectedItem.ZipCode;
            }
        }
    }
}