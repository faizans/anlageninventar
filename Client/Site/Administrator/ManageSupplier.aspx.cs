using Client.Site.Controls.ListBox2;
using Client.SiteMaster;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.Site.Administrator {
    public partial class ManageSupplier : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            loadPage();
        }

        #region Properties

        private Supplier supplier {
            get {
                return Session["Supplier"] as Supplier;
            }
            set {
                Session["Supplier"] = value;
            }
        }

        #endregion

        #region Initialisation

        private void loadPage() {
            if (!IsPostBack) {
                getParameters();
                bindSupplierData();
            }
        }

        private void getParameters() {
            this.supplier = null;
            if (Request.QueryString["si"] != null && Request.QueryString["si"] != "") {
                int supplierId = int.Parse(Request.QueryString["si"]);
                this.supplier = Supplier.GetById(supplierId);
            }
        }

        private void bindSupplierData() {
            if (this.supplier != null) {
                this.rtbName.Text = this.supplier.Name;

                if (this.supplier.SupplierBranches.Any()) {
                    this.ListBoxControl.ListItems = this.supplier.SupplierBranches.Select(s => new ListBoxItem(s.ZipCode + " " + s.Place, s.SupplierBranchId.ToString(), s)).ToList();
                    this.ListBoxControl.DataSource = this.ListBoxControl.ListItems;
                }
            } else {
                this.supplier = new Supplier();
                EntityFactory.Context.Suppliers.Add(supplier);
            }
        }

        #endregion

        /// <summary>
        /// Get the removed items and delete them after saving
        /// </summary>
        private void handleSupplicerBranchesToDelete() {
            if (this.ListBoxControl.ItemsToDelete.Any()) {
                foreach (ListBoxItem listboxItemToDelete in this.ListBoxControl.ItemsToDelete) {
                    if (!listboxItemToDelete.Value.Contains("-")) {
                        SupplierBranch itemToDelete = SupplierBranch.GetById(int.Parse(listboxItemToDelete.Value));
                        this.supplier.SupplierBranches.Remove(itemToDelete);
                        itemToDelete.Delete();
                    }
                }
                this.ListBoxControl.ItemsToDelete = null;
            }
        }

        private void save() {
            handleSupplicerBranchesToDelete();
            this.supplier.Name = this.rtbName.Text;
            EntityFactory.Context.SaveChanges();
        }

        #region Events

        protected void ListBoxControl_SelectedIndexChanged(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs.Item != null && eventArgs.Item.DataItem != null) {
                SupplierBranch selectedBranch = eventArgs.Item.DataItem as SupplierBranch;
                this.rtbBranchPlace.Text = selectedBranch.Place;
                this.rtbBranchPlz.Text = selectedBranch.ZipCode;
            }
        }

        protected void ListBoxControl_AddNewItem(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs.Item != null) {
                SupplierBranch selectedBranch = eventArgs.Item.DataItem as SupplierBranch;
                if (selectedBranch == null || SupplierBranch.GetById(selectedBranch.SupplierBranchId) == null) {
                    selectedBranch = new SupplierBranch();
                    this.ListBoxControl.SelectedListBoxItem.DataItem = selectedBranch;
                    this.supplier.SupplierBranches.Add(selectedBranch);
                } else {
                }
                this.rtbBranchPlace.Text = selectedBranch.Place;
                this.rtbBranchPlz.Text = selectedBranch.ZipCode;
            }
        }

        protected void ListBoxControl_ItemRemove(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            this.rtbBranchPlace.Text = null;
            this.rtbBranchPlz.Text = null;
        }

        #region FormEvents

        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("~/Site/Administrator/SupplierList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            Response.Redirect("~/Site/Administrator/SupplierList.aspx");
        }

        protected void rtbBranchPlz_TextChanged(object sender, EventArgs e) {
            if (this.ListBoxControl.SelectedListBoxItem != null && this.ListBoxControl.SelectedListBoxItem.DataItem != null) {
                SupplierBranch selectedBranch = this.ListBoxControl.SelectedListBoxItem.DataItem as SupplierBranch;
                selectedBranch.Place = this.rtbBranchPlace.Text;
                selectedBranch.ZipCode = this.rtbBranchPlz.Text;
                this.ListBoxControl.SelectedItem.Text = this.rtbBranchPlz.Text + " " + this.rtbBranchPlace.Text;
            }
        }

        #endregion

        #endregion
    }
}