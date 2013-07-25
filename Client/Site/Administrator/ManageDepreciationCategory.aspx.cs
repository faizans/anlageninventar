using Client.Site.Controls.ListBox2;
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
            loadPage();
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
                this.ListBoxControl.ItemsToDelete = null;
                this.ListBoxControl.ListItems = null;
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

                if (this.depreciationCategory.Depreciations.Any()) {
                    this.ListBoxControl.ListItems = this.depreciationCategory.Depreciations.Select(s => new ListBoxItem(s.AdditionalStartDate.Value.Year.ToString() + " " + s.AdditionalEndDate.Value.Year.ToString(), s.DepreciationId.ToString(), s)).ToList();
                    this.ListBoxControl.DataSource = this.ListBoxControl.ListItems;
                }
            } else {
                this.depreciationCategory = new DepreciationCategory();
                EntityFactory.Context.DepreciationCategories.Add(depreciationCategory);
            }
        }

        #endregion

        /// <summary>
        /// Get the removed items and delete them after saving
        /// </summary>
        private void handleItemsToDelete() {
            if (this.ListBoxControl.ItemsToDelete.Any()) {
                foreach (ListBoxItem listboxItemToDelete in this.ListBoxControl.ItemsToDelete) {
                    if (!listboxItemToDelete.Value.Contains("-")) {
                        Depreciation itemToDelete = Depreciation.GetById(int.Parse(listboxItemToDelete.Value));
                        this.depreciationCategory.Depreciations.Remove(itemToDelete);
                        itemToDelete.Delete();
                    }
                }
                this.ListBoxControl.ItemsToDelete = null;
            }
        }

        private void save() {
            handleItemsToDelete();
            this.depreciationCategory.Name = this.rtbName.Text;
            EntityFactory.Context.SaveChanges();
        }

        #region Events

        protected void ListBoxControl_SelectedIndexChanged(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs.Item != null && eventArgs.Item.DataItem != null) {
                enableDepreciationForms(true);
                Depreciation depreciation = eventArgs.Item.DataItem as Depreciation;
                this.rtbFromYear.Text = depreciation.AdditionalStartDate.HasValue ? depreciation.AdditionalStartDate.Value.Year.ToString() : "";
                this.rtbToYear.Text = depreciation.AdditionalEndDate.HasValue ? depreciation.AdditionalEndDate.Value.Year.ToString() : "";
            } else {
                enableDepreciationForms(false);
            }
        }

        private void enableDepreciationForms(Boolean enabled) {
            this.btnApply.Enabled = enabled;
            this.rtbFromYear.Enabled = enabled;
            this.rtbToYear.Enabled = enabled;
        }

        protected void ListBoxControl_AddNewItem(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs.Item != null) {
                Depreciation selectedDepreciation = eventArgs.Item.DataItem as Depreciation;
                if (selectedDepreciation == null || Depreciation.GetById(selectedDepreciation.DepreciationId) == null) {
                    selectedDepreciation = new Depreciation();
                    this.ListBoxControl.SelectedListBoxItem.DataItem = selectedDepreciation;
                    this.depreciationCategory.Depreciations.Add(selectedDepreciation);
                } else {
                }
                this.rtbFromYear.Text = selectedDepreciation.AdditionalStartDate.HasValue ? selectedDepreciation.AdditionalStartDate.Value.Year.ToString() : "";
                this.rtbToYear.Text = selectedDepreciation.AdditionalEndDate.HasValue ? selectedDepreciation.AdditionalEndDate.Value.Year.ToString() : "";
            }
        }

        protected void ListBoxControl_ItemRemove(object sender, EventArgs e) {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs != null && eventArgs.Item != null) {
                Depreciation depreciationToRemove = eventArgs.Item.DataItem as Depreciation;
                if (depreciationToRemove != null) {
                    if (depreciationToRemove.HasArticles()) {
                        RadWindowManager1.RadAlert(String.Format("Abschreibung kann nicht gelöscht werden, da Abschreibung von Artikeln verwendet wird."), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    } else {
                        this.ListBoxControl.Remove(eventArgs.Item);
                        this.rtbFromYear.Text = null;
                        this.rtbToYear.Text = null;
                        this.enableDepreciationForms(false);
                    }
                } else {
                    this.ListBoxControl.Remove(eventArgs.Item);
                    this.rtbFromYear.Text = null;
                    this.rtbToYear.Text = null;
                    this.enableDepreciationForms(false);
                }
            } 
        }

        #region FormEvents

        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("~/Site/Administrator/DepreciationCategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            Response.Redirect("~/Site/Administrator/DepreciationCategoryList.aspx");
        }

        protected void btnApply_Click(object sender, EventArgs e) {
            updateItem();
        }

        private void updateItem() {
            if (this.ListBoxControl.SelectedListBoxItem != null && this.ListBoxControl.SelectedListBoxItem.DataItem != null) {
                Depreciation selectedDepreciation = this.ListBoxControl.SelectedListBoxItem.DataItem as Depreciation;
                if (this.rtbFromYear.Text.Count() == 4) {
                    selectedDepreciation.AdditionalStartDate = new DateTime(int.Parse(this.rtbFromYear.Text), 1, 1);
                }
                if (this.rtbToYear.Text.Count() == 4) {
                    selectedDepreciation.AdditionalEndDate = new DateTime(int.Parse(this.rtbToYear.Text), 12, 31);
                }
                this.ListBoxControl.SelectedItem.Text = this.rtbFromYear.Text + " " + this.rtbToYear.Text;
            }
        }

        #endregion


        #endregion
    }
}