using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Linq.Dynamic;

namespace Client.Site.Controls.CustomGrids {
    public class ArticleGridHelper {

        public String[] NonEntityFields = { "DepreciationValue", "RoomPath" };

        public static List<Article> GetReportItems(RadGrid rgGrid, List<Article> reportSource, Boolean HasFooter) {

            ArticleGridHelper gridHelper = new ArticleGridHelper();

            if (!rgGrid.MasterTableView.FilterExpression.Any()) {

            } else {
                //Get the filter expression
                String filterExpression = rgGrid.MasterTableView.FilterExpression;
                String filter = null;
                String specialFilter = null;

                //HAndle speical chars
                filterExpression = filterExpression.Replace("[", "");
                filterExpression = filterExpression.Replace("]", "");
                filterExpression = filterExpression.Replace("(", "");
                filterExpression = filterExpression.Replace(")", "");
                filterExpression = filterExpression.Replace("%", "");
                filterExpression = filterExpression.Replace("'", "\"");


                if (filterExpression.Contains("AND")) {

                    //Spkit the filter in Entity and NonEntity parts
                    List<String> queryParts = filterExpression.Split(new string[] { "AND" }, StringSplitOptions.None).ToList();
                    List<String> nonEntityParts = new List<String>();
                    foreach (String nonEntityField in gridHelper.NonEntityFields) {
                        List<String> matches = new List<String>(queryParts.Where(p => p.Contains(nonEntityField)));
                        if (matches.Any()) {
                            foreach (String match in matches) {
                                nonEntityParts.Add(match);
                                queryParts.Remove(match);
                            }
                        }
                    }
                    //Connect the parts with AND
                    int counter = 0;
                    while (counter <= queryParts.Count - 1) {
                        filter += gridHelper.HandleSpecialArguments(queryParts[counter]);
                        if (counter < queryParts.Count - 1) {
                            filter += " AND ";
                        }
                        counter++;
                    }

                    if (filter.Any()) {
                        reportSource = Article.GetAvailable().AsQueryable().Where(filter).ToList();
                    }

                    if (nonEntityParts.Any()) {
                        if (!reportSource.Any()) {
                            reportSource = Article.GetAvailable().ToList();
                        }
                        foreach (String specialQuery in nonEntityParts) {
                            reportSource = gridHelper.FilterSource(reportSource, specialQuery);
                        }
                    }

                } else {
                    foreach (String nonEntityField in gridHelper.NonEntityFields) {
                        if (filterExpression.Contains(nonEntityField)) {
                            specialFilter = gridHelper.HandleSpecialArguments(filterExpression);
                            break;
                        }
                    }
                    if (specialFilter != null) {
                        reportSource = gridHelper.FilterSource(Article.GetAvailable().ToList(), gridHelper.HandleSpecialArguments(specialFilter));
                    } else {
                        reportSource = gridHelper.FilterSource(Article.GetAvailable().ToList(), gridHelper.HandleSpecialArguments(filterExpression));
                    }
                }
            }
            if (HasFooter) {
                if (rgGrid.MasterTableView.GetItems(GridItemType.Footer) != null) {
                    GridFooterItem dataItem = rgGrid.MasterTableView.GetItems(GridItemType.Footer)[0] as GridFooterItem;
                    Article article = new Article();
                    article.Name = "Total:";
                    article.Value = double.Parse(dataItem["Value"].Text);
                    article.DepreciationValue = double.Parse(dataItem["DepreciationValue"].Text);
                    reportSource.Add(article);
                }
            }

            return reportSource;
        }

        public String HandleSpecialArguments(String queryPart) {
            if (queryPart.Contains("LIKE")) {
                String[] qparams = queryPart.Split("LIKE".ToCharArray());
                qparams[0] = qparams[0].Replace(" ", "");
                queryPart = queryPart.Replace(queryPart, qparams[0] + ".Contains(" + qparams[4] + ")");
            }
            return queryPart;
        }

        public List<Article> FilterSource(List<Article> source, String argument) {
            return source.AsQueryable().Where(this.HandleSpecialArguments(argument)).ToList();
        }

        public static void ClearFilter(RadGrid rgGrid) {
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