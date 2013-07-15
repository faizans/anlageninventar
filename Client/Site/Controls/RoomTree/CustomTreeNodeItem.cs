using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace Client.Site.Controls.RoomTree
{
    public class CustomTreeNodeItem : RadTreeNode
    {

        public CustomTreeNodeItem()
        {

        }

        public CustomTreeNodeItem(int id, int parentId, String text, object dataItem)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.Text = text;
            this.DataItem = dataItem;
        }

        public CustomTreeNodeItem(String text)
        {
            this.Id = -1;
            this.ParentId = 0;
            this.Text = text;
            this.IsRoot = true;
        }

        public CustomTreeNodeItem(int id, int parentId, String text, object dataItem, Boolean IsRoot)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.DataItem = dataItem;
            this.IsRoot = IsRoot;
            this.Text = text;
        }

        public string Uid { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public object DataItem { get; set; }
        public bool IsRoot { get; set; }
        public string Responsible { get; set; }

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