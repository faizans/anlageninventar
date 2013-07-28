using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Linq.Dynamic;

namespace Client.Site.Controls.CustomGrids {
    public static class GridHelper {
        public static List<Article> GetReportItems(RadGrid rgGrid) {
            List<Article> reportSource = new List<Article>();
            if (!rgGrid.MasterTableView.FilterExpression.Any()) {
                reportSource = Article.GetAvailable().ToList();
            } else {
                //Get the filter expression
                String filter = rgGrid.MasterTableView.FilterExpression;

                //HAndle speical chars
                filter = filter.Replace("[", "");
                filter = filter.Replace("]", "");
                filter = filter.Replace("(", "");
                filter = filter.Replace(")", "");
                filter = filter.Replace("%", "");
                filter = filter.Replace("'", "\"");

                //Handle expressions
                if (filter.Contains("AND")) {
                    foreach (String queryPart in filter.Split("AND".ToCharArray())) {
                        filter = handleArguments(filter, queryPart);
                    }
                } else {
                    filter = handleArguments(filter, filter);
                }

                reportSource = Article.GetAvailable().AsQueryable().Where(filter).ToList();

                if (rgGrid.MasterTableView.GetItems(GridItemType.TFoot).Any()) {
                    GridDataItem dataItem = rgGrid.MasterTableView.GetItems(GridItemType.TFoot)[0] as GridDataItem;
                    Article article = new Article();
                    article.Name = "Total:";
                    article.Value = double.Parse(dataItem["Value"].Text);
                    reportSource.Add(article);
                }
            }
            return reportSource;
        }

        public static String handleArguments(String filter, String queryPart) {
            if (queryPart.Contains("LIKE")) {
                String[] qparams = queryPart.Split("LIKE".ToCharArray());
                qparams[0] = qparams[0].Replace(" ", "");
                filter = filter.Replace(queryPart, qparams[0] + ".Contains(" + qparams[4] + ")");
            }
            return filter;
        }

        public static void ClearFilter(RadGrid rgGrid){
            GridFilterMenu menu = rgGrid.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count) {
                if (menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith" || menu.Items[i].Text == "Between"
                 || menu.Items[i].Text == "NotBetween" || menu.Items[i].Text == "IsEmpty" || menu.Items[i].Text == "NotIsEmpty" || menu.Items[i].Text == "IsNull" || menu.Items[i].Text == "NotIsNull") {
                    menu.Items.RemoveAt(i);
                } else {
                    i++;
                }
            }
        }
    }
}