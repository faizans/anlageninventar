using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace Client.Site.Controls.RoomTree
{
    public class RoomTreeItem : RadTreeNode
    {

        public RoomTreeItem()
        {

        }

        public RoomTreeItem(String text, String value, object dataItem) {
            this.Text = text;
            this.Value = value;
            this.DataItem = dataItem;
        }

        public RoomTreeItem(int itemId, int parentId, String text, String value, object dataItem)
        {
            this.Id = itemId;
            this.ParentId = parentId;
            this.Text = text;
            this.Value = value;
            this.DataItem = dataItem;
        }

        public RoomTreeItem(String text)
        {
            this.Id = -1;
            this.Value = null;
            this.ParentId = 0;
            this.Text = text;
            this.IsRoot = true;
            base.Checkable = false;
        }

        public RoomTreeItem(int itemId, int parentId, String text, String value, object dataItem, Boolean IsRoot)
        {
            this.Id = itemId;
            this.ParentId = parentId;
            this.DataItem = dataItem;
            this.Value = value;
            this.Text = text;
        }

        public string Uid { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public object DataItem { get; set; }
        public bool IsRoot { get; set; }
        public string Responsible { get; set; }
        //public RoomTreeItem ParentNode { get; set; }
        public bool IsNew = false;

        public void AddAttributesTo(RadTreeNode node)
        {
            if (this.DataItem != null)
            {
                node.Attributes["DataItemType"] = ObjectContext.GetObjectType(this.DataItem.GetType()).ToString();
            }
            node.Attributes["IsRoot"] = this.IsRoot.ToString();
            node.Attributes["Id"] = this.Id.ToString();
            node.Attributes["ParentId"] = this.ParentId.ToString();
            node.Attributes["Responsible"] = this.Responsible;
        }
    }
}