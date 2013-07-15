using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Administrator
{
    public partial class ArticleCategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Events
        protected void rgArticleCategories_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem dataItem = e.Item as GridDataItem;

                    ArticleCategory categoryToDelete = ArticleCategory.GetById(int.Parse(dataItem.GetDataKeyValue("ArticleCategoryId").ToString()));
                    if (categoryToDelete.Articles.Any())
                    {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel mit dieser Kategorie versehen sind.", categoryToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    }
                    else
                    {
                        //TODO
                        categoryToDelete.Delete();
                    }
                }
            }
        }
        #endregion
    }
}