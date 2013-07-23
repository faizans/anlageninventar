using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Controls.ListBox2
{
    public partial class ListBoxControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Properties

        public List<ListBoxItem> ItemsToDelete
        {
            get
            {
                if (Session["ItemsToDelete"] == null)
                {
                    Session["ItemsToDelete"] = new List<ListBoxItem>();
                }
                return Session["ItemsToDelete"] as List<ListBoxItem>;
            }
            set
            {
                Session["ItemsToDelete"] = value;
            }
        }

        public List<ListBoxItem> ListItems
        {
            get
            {
                if (Session["ListItems"] == null)
                {
                    Session["ListItems"] = new List<ListBoxItem>();
                }
                return Session["ListItems"] as List<ListBoxItem>;
            }
            set{
                Session["ListItems"] = value;
            }
        }

        public ListBoxItem SelectedListBoxItem
        {
            get
            {
                if (this.ListBox.SelectedItem != null) {
                    return this.ListItems.Where(i => i.Value == this.ListBox.SelectedItem.Value).SingleOrDefault();
                } else {
                    return this.ListItems.ElementAt(this.ListItems.Count - 1);
                }

            }
        }

        public RadListBoxItem SelectedItem
        {
            get
            {
                if (this.ListBox.SelectedItem != null)
                {
                    return this.ListBox.SelectedItem;
                } else {
                    return this.ListBox.Items.ElementAt(0);
                }
            }
        }

        public List<ListBoxItem> DataSource
        {
            set {
                this.ListBox.DataSource = value;
                this.ListBox.DataBind();
            }
        }

        #endregion

        #region Events

        public event EventHandler AddNewItem;
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ListBoxItem newItem = new ListBoxItem("Neues Item", Guid.NewGuid().ToString(), null);
            this.ListItems.Add(newItem);
            this.ListBox.Items.Insert(0,newItem);

            if (AddNewItem != null)
            {
                ListBoxItemEventArgs listBoxItemArgs = new ListBoxItemEventArgs();
                listBoxItemArgs.Item = newItem;
                AddNewItem(this, listBoxItemArgs);
            }
        }

        public event EventHandler ItemRemove;
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            ListBoxItem itemToRemove = this.SelectedListBoxItem;
            this.ItemsToDelete.Add(itemToRemove);
            this.ListItems.Remove(itemToRemove);
            this.ListBox.Items.Remove(this.ListBox.SelectedItem);

            if (ItemRemove != null)
            {
                ListBoxItemEventArgs listBoxItemArgs = new ListBoxItemEventArgs();
                listBoxItemArgs.Item = itemToRemove;
                ItemRemove(this, listBoxItemArgs);
            }
        }

        public event EventHandler SelectedIndexChanged;
        protected void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
            {
                ListBoxItemEventArgs listBoxItemArgs = new ListBoxItemEventArgs();
                listBoxItemArgs.Item = this.SelectedListBoxItem;
                SelectedIndexChanged(this, listBoxItemArgs);
            }
        }

        #endregion
    }
}