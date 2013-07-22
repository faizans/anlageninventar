using Client.Site.Controls.ListBox2;
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
    public partial class ManageDepreciationCategory : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            loadPage();
        }

        #region Properties

        private DepreciationCategory depreciationCategory
        {
            get
            {
                return Session["DepreciationCategory"] as DepreciationCategory;
            }
            set
            {
                Session["DepreciationCategory"] = value;
            }
        }

        #endregion

        #region Initialisation

        private void loadPage()
        {
            if (!IsPostBack)
            {
                getParameters();
                bindData();
            }
        }

        private void getParameters()
        {
            if (Request.QueryString["ci"] != null && Request.QueryString["ci"] != "")
            {
                int categoryId = int.Parse(Request.QueryString["ci"]);
                this.depreciationCategory = DepreciationCategory.GetById(categoryId);
            }
        }

        private void bindData()
        {
            if (this.depreciationCategory != null)
            {
                this.rtbName.Text = this.depreciationCategory.Name;

                if (this.depreciationCategory.Depreciations.Any())
                {
                    this.ListBoxControl.ListItems = this.depreciationCategory.Depreciations.Select(s => new ListBoxItem(s.Value + " " + s.Year, s.DepreciationCategoryId.ToString(), s)).ToList();
                    this.ListBoxControl.DataSource = this.ListBoxControl.ListItems;
                }
            }
            else
            {
                this.depreciationCategory = new DepreciationCategory();
                EntityFactory.Context.DepreciationCategories.Add(depreciationCategory);
            }
        }

        #endregion

        /// <summary>
        /// Get the removed items and delete them after saving
        /// </summary>
        private void handleItemsToDelete()
        {
            if (this.ListBoxControl.ItemsToDelete.Any())
            {
                foreach (ListBoxItem listboxItemToDelete in this.ListBoxControl.ItemsToDelete)
                {
                    if (!listboxItemToDelete.Value.Contains("-"))
                    {
                        Depreciation itemToDelete = Depreciation.GetById(int.Parse(listboxItemToDelete.Value));
                        this.depreciationCategory.Depreciations.Remove(itemToDelete);
                        itemToDelete.Delete();
                    }
                }
                this.ListBoxControl.ItemsToDelete = null;
            }
        }

        private void save()
        {
            handleItemsToDelete();
            this.depreciationCategory.Name = this.rtbName.Text;
            EntityFactory.Context.SaveChanges();
        }

        #region Events

        protected void ListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs.Item != null && eventArgs.Item.DataItem != null)
            {
                Depreciation depreciation = eventArgs.Item.DataItem as Depreciation;
                this.rtbValue.Text = depreciation.Value.ToString();
                this.rtbYear.Text = depreciation.Year.ToString();
            }
        }

        protected void ListBoxControl_AddNewItem(object sender, EventArgs e)
        {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            if (eventArgs.Item != null)
            {
                Depreciation selectedDepreciation = eventArgs.Item.DataItem as Depreciation;
                if (Depreciation.GetById(selectedDepreciation.DepreciationId) == null)
                {
                    this.depreciationCategory.Depreciations.Add(selectedDepreciation);
                }
                this.rtbValue.Text = selectedDepreciation.Value.ToString();
                this.rtbYear.Text = selectedDepreciation.Year.ToString();
            }
        }

        protected void ListBoxControl_ItemRemove(object sender, EventArgs e)
        {
            ListBoxItemEventArgs eventArgs = e as ListBoxItemEventArgs;
            this.rtbValue.Text = null;
            this.rtbYear.Text = null;
        }

        #region FormEvents

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Site/Administrator/DepreciationCategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            save();
            Response.Redirect("~/Site/Administrator/DepreciationCategoryList.aspx");
        }

        protected void rtbValue_TextChanged(object sender, EventArgs e)
        {
            if (this.ListBoxControl.SelectedListBoxItem != null && this.ListBoxControl.SelectedListBoxItem.DataItem != null)
            {
                Depreciation selectedDepreciation = this.ListBoxControl.SelectedListBoxItem.DataItem as Depreciation;
                selectedDepreciation.Value = int.Parse(this.rtbValue.Text);
                selectedDepreciation.Year = int.Parse(this.rtbYear.Text);
                this.ListBoxControl.SelectedItem.Text = this.rtbValue.Text + " " + this.rtbYear.Text;
            }
        }

        #endregion

        #endregion
    }
}