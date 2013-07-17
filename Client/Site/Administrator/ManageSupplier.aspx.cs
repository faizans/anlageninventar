
using Client.Site.Controls.ListBox;
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
                bindSupplierData();
            }
        }

        private void getParameters()
        {
            if (Request.QueryString["si"] != null && Request.QueryString["si"] != "")
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
                    this.ListBoxControl.DataSource = this.supplier.SupplierBranches.Select(s => new ListBoxItem(s.ZipCode + " - " + s.Place, s.SupplierBranchId.ToString(), s)).ToList();
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
            else
            {
                this.rtbBranchPlace.Text = null;
                this.rtbBranchPlz.Text = null;
            }
        }

        private void save()
        {
            this.supplier.Name = this.rtbName.Text;

            //Do the supplier branches stuff
            this.supplier.SupplierBranches = getSupplierBranchesToSave();
            this.handleSupplicerBranchesToDelete();

            //Save context
            EntityFactory.Context.SaveChanges();
        }

        private void handleSupplicerBranchesToDelete()
        {
            if (this.ListBoxControl.ItemsToDelete.Any()) {
                foreach (RadListBoxItem listboxItemToDelete in this.ListBoxControl.ItemsToDelete)
                {
                    SupplierBranch itemToDelete = SupplierBranch.GetById(int.Parse(listboxItemToDelete.Value));
                    itemToDelete.Delete();
                }
            }
        }

        private List<SupplierBranch> getSupplierBranchesToSave()
        {
            List<RadListBoxItem> listBoxItems = this.ListBoxControl.GetItems();
            List<SupplierBranch> supplierBranchesToSave = new List<SupplierBranch>();
            if (listBoxItems.Any())
            {
                foreach (RadListBoxItem listBoxItem in listBoxItems)
                {
                    int supplierBranchId;
                    bool result = Int32.TryParse(listBoxItem.Value, out supplierBranchId);
                    SupplierBranch branchToSave;
                    if (result)
                    {
                        branchToSave = SupplierBranch.GetById(supplierBranchId);
                        if (branchToSave == null)
                        {
                            branchToSave = new SupplierBranch();
                            EntityFactory.Context.SupplierBranches.Add(branchToSave);
                        }
                    }
                    else
                    {
                        branchToSave = new SupplierBranch();
                        EntityFactory.Context.SupplierBranches.Add(branchToSave);
                    }

                    supplierBranchesToSave.Add(branchToSave);
                }
            }
            return supplierBranchesToSave;
        }

        #region Events

        protected void ListBoxControl_NewItemListBoxItemAdded(object sender, EventArgs e)
        {
            this.rtbBranchPlace.Focus();
        }

        protected void ListBoxControl_SelectedListBoxItemChanged(object sender, EventArgs e)
        {
            this.selectedBranch = null;
            RadListBoxItem currentItem = (e as ListBoxItemEventArgs).Item;
            if (currentItem.Value != null)
            {
                int supplierBranchId;
                bool result = Int32.TryParse(currentItem.Value, out supplierBranchId);
                if (result)
                {
                    SupplierBranch supplierBranch = SupplierBranch.GetById(supplierBranchId);
                    this.selectedBranch = supplierBranch;
                }
            }
            updateBranch();
        }

        protected void rtbBranchPlace_TextChanged(object sender, EventArgs e)
        {
            updateListBoxItem();
        }

        public void updateListBoxItem()
        {
            this.ListBoxControl.SelectedItem.Text = this.rtbBranchPlz.Text + " - " + this.rtbBranchPlace.Text;
            //Get data item if null create new save attributes
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Site/Administrator/SupplierList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        #endregion

    }
}