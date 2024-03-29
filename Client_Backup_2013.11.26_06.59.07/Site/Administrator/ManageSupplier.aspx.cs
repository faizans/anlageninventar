﻿using Client.Site.Controls.ListBox2;
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
    public partial class ManageSupplier : System.Web.UI.Page {

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
                enableBranchForms(true);
                SupplierBranch selectedBranch = eventArgs.Item.DataItem as SupplierBranch;

                this.rtbBranchPlace.Text = selectedBranch.Place;
                this.rtbBranchPlz.Text = selectedBranch.ZipCode;
                this.rtbComment.Text = selectedBranch.Comment;

            } else {
                enableBranchForms(false);
            }
        }

        private void enableBranchForms(Boolean enabled) {
            this.btnApply.Enabled = enabled;
            this.rtbBranchPlace.Enabled = enabled;
            this.rtbBranchPlz.Enabled = enabled;
            this.rtbComment.Enabled = enabled;


            this.btnSave.Enabled = !enabled;
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
                this.rtbComment.Text = selectedBranch.Comment;
            }
            enableBranchForms(true);
        }

        protected void ListBoxControl_ItemRemove(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs != null && eventArgs.Item != null) {
                SupplierBranch supplierBranchToRemove = eventArgs.Item.DataItem as SupplierBranch;
                if (supplierBranchToRemove != null) {
                    if (supplierBranchToRemove.HasArticles()) {
                        RadWindowManager1.RadAlert(String.Format("Standort kann nicht gelöscht werden, da Standort von Artikeln verwendet wird."), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        this.ListBoxControl.Remove(eventArgs.Item);
                        this.supplier.SupplierBranches.Remove(supplierBranchToRemove);
                        this.rtbBranchPlace.Text = null;
                        this.rtbBranchPlz.Text = null;
                        this.rtbComment.Text = null;
                        this.enableBranchForms(false);
                    }
                } else {
                    this.ListBoxControl.Remove(eventArgs.Item);
                    this.rtbBranchPlace.Text = null;
                    this.rtbBranchPlz.Text = null;
                    this.rtbComment.Text = null;
                    this.enableBranchForms(false);
                }
            } 
        }

        private void back() {
            if (Request.QueryString["back"] != null) {
                Response.Redirect("~/Site/Administrator/" + Request.QueryString["back"] + ".aspx");
            } else {
                Response.Redirect("~/Site/Administrator/SupplierList.aspx");
            }
        }

        #region FormEvents

        protected void btnBack_Click(object sender, EventArgs e) {
            back();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            back();
        }

        public void UpdateItem() {
            if (this.ListBoxControl.SelectedListBoxItem != null) {
                SupplierBranch selectedBranch = this.ListBoxControl.SelectedListBoxItem.DataItem != null ? this.ListBoxControl.SelectedListBoxItem.DataItem as SupplierBranch : new SupplierBranch();
                selectedBranch.Place = this.rtbBranchPlace.Text;
                selectedBranch.ZipCode = this.rtbBranchPlz.Text;
                selectedBranch.Comment = this.rtbComment.Text;
                this.ListBoxControl.SelectedListBoxItem.DataItem = selectedBranch;
                this.ListBoxControl.SelectedItem.Text = this.rtbBranchPlz.Text + " " + this.rtbBranchPlace.Text;
                this.ListBoxControl.AppliedData = true;
            }
        }

        protected void btnApply_Click(object sender, EventArgs e) {
            UpdateItem();
            this.btnSave.Enabled = true;
        }

        #endregion

        #endregion
    }
}