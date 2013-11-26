using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace Client.Site.Controls.ListBox2
{
    public class ListBoxItem : RadListBoxItem
    
    {
        public ListBoxItem()
        {
        }

        public ListBoxItem(String text, String value, object dataItem)
        {
            this.Text = text;
            this.Value = value;
            this.DataItem = dataItem;
        }
    }
}