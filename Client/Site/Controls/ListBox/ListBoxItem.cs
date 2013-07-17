using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace Client.Site.Controls.ListBox
{
    public class ListBoxItem : RadListBoxItem
    {
        public ListBoxItem()
        {
        }

        public ListBoxItem(String name, String value, object dataItem)
        {
            this.Text = name;
            this.Value = value;
            this.DataItem = dataItem;
        }
    }
}