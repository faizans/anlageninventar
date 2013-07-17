using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Controls.ListBox
{
    public partial class ListBoxControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Properties

        private List<ListBoxItem> dataSource;
        public List<ListBoxItem> DataSource
        {
            get
            {
                if (this.dataSource == null)
                {
                    this.dataSource = new List<ListBoxItem>();
                }
                return this.dataSource;
            }
            set
            {
                this.dataSource = value;
                this.ListBox.DataSource = dataSource;
                this.ListBox.DataBind();
            }
        }

        private List<RadListBoxItem> itemsToDelete;
        public List<RadListBoxItem> ItemsToDelete
        {
            get
            {
                if (itemsToDelete == null)
                {
                    itemsToDelete = new List<RadListBoxItem>();
                }
                return itemsToDelete;
            }
        }

        public RadListBoxItem SelectedItem
        {
            get
            {
                return this.ListBox.SelectedItem;
            }
        }

        #endregion

        #region methods

        public List<RadListBoxItem> GetItems()
        {
            return this.ListBox.Items.ToList();
        }

        #endregion

        #region Events

        public event EventHandler NewItemListBoxItemAdded;
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ListBoxItem newItem = new ListBoxItem("Neues Item", null, null);
            this.ListBox.Items.Insert(0, newItem);

            if (NewItemListBoxItemAdded != null)
            {
                ListBoxItemEventArgs listBoxItemArgs = new ListBoxItemEventArgs();
                listBoxItemArgs.Item = newItem;
                NewItemListBoxItemAdded(this, listBoxItemArgs);
            }

            this.ListBox.SelectedIndex = 0;
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.SelectedItem != null)
            {
                int supplierBranchId;
                bool result = Int32.TryParse(this.SelectedItem.Value, out supplierBranchId);

                if (result)
                {
                    this.ItemsToDelete.Add(this.SelectedItem);
                }

                this.SelectedItem.Remove();
            }
        }

        public event EventHandler SelectedListBoxItemChanged;
        protected void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedListBoxItemChanged != null)
            {
                ListBoxItemEventArgs listBoxItemArgs = new ListBoxItemEventArgs();
                listBoxItemArgs.Item = this.ListBox.SelectedItem;
                SelectedListBoxItemChanged(this, listBoxItemArgs);
            }
        }

        #endregion
    }
}