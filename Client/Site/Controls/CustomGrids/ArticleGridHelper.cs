using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Linq.Dynamic;

namespace Client.Site.Controls.CustomGrids {
    /// <summary>
    /// This Class is meant to filter the grids datasources with dynamic linq queries
    /// It is also here to minimize filter options and rename the english standard filters to german
    /// </summary>
    public class ArticleGridHelper {

        /// <summary>
        /// Apply the grids filter expression to the reportsource (uses dynamic linq)
        /// </summary>
        /// <param name="rgGrid">The grid to operate on</param>
        /// <param name="reportSource">The gridsource where to apply the filterexpression</param>
        /// <param name="HasFooter">Set to true if grid has a footer</param>
        /// <returns></returns>
        public static List<Article> GetReportItems(RadGrid rgGrid, List<Article> reportSource, Boolean HasFooter) {

            if (rgGrid.MasterTableView.FilterExpression.Any()) {
                String filterExpression = rgGrid.MasterTableView.FilterExpression;
                reportSource = reportSource.Where(a => a.DepreciationCategory != null 
                    && a.InsuranceCategory != null
                    ).AsQueryable().Where(filterExpression).ToList();
            }

            if (HasFooter) {
                if (rgGrid.MasterTableView.GetItems(GridItemType.Footer) != null) {
                    GridFooterItem dataItem = rgGrid.MasterTableView.GetItems(GridItemType.Footer)[0] as GridFooterItem;
                    Article article = new Article();
                    article.Name = "Total:";
                    article.Value = double.Parse(dataItem["Value"].Text);
                    article.DepreciationValue = double.Parse(dataItem["DepreciationValue"].Text);
                    if (reportSource.ElementAt(reportSource.Count - 1).Name == "Total:") {
                        reportSource.Remove(reportSource.ElementAt(reportSource.Count - 1));
                    }
                    reportSource.Add(article);
                }
            }

            return reportSource;
        }

        /// <summary>
        /// Remove some filter options and rename the remaining ones with german translation
        /// </summary>
        /// <param name="rgGrid">The grid to operate on</param>
        public static void ClearFilter(RadGrid rgGrid) {
            GridFilterMenu menu = rgGrid.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count) {
                if (menu.Items[i].Text == "StartsWith" || menu.Items[i].Text == "EndsWith" || menu.Items[i].Text == "Between"
                 || menu.Items[i].Text == "NotBetween" || menu.Items[i].Text == "IsEmpty" || menu.Items[i].Text == "NotIsEmpty" || menu.Items[i].Text == "IsNull" || menu.Items[i].Text == "NotIsNull") {
                    menu.Items.RemoveAt(i);
                } else {
                    switch (menu.Items[i].Text) {
                        case "NoFilter":
                            menu.Items[i].Text = "Kein Filter";
                            break;
                        case "Contains":
                            menu.Items[i].Text = "Beinhaltet";
                            break;
                        case "DoesNotContain":
                            menu.Items[i].Text = "Beinhaltet nicht";
                            break;
                        case "EqualTo":
                            menu.Items[i].Text = "Gleich";
                            break;
                        case "NotEqualTo":
                            menu.Items[i].Text = "Ungleich";
                            break;
                        case "GreaterThan":
                            menu.Items[i].Text = "Grösser als";
                            break;
                        case "LessThan":
                            menu.Items[i].Text = "Kleiner als";
                            break;
                        case "GreaterThanOrEqualTo":
                            menu.Items[i].Text = "Grösser als oder gleich";
                            break;
                        case "LessThanOrEqualTo":
                            menu.Items[i].Text = "Kleiner als oder gleich";
                            break;
                    }
                    i++;
                }
            }
        }

        public static List<Article> GroupReportArticles(List<Article> source, bool? useStoredValues) {
            List<Article> result = new List<Article>();
            IEnumerable<IEnumerable<Article>> groupedSource = source.GroupBy(s => s.Barcode.Split('.')[0]);
            if (groupedSource != null) {
                foreach (IEnumerable<Article> group in groupedSource) {

                    Article groupedArticle = new Article();
                    groupedArticle.Name = group.ElementAt(0).Name;
                    groupedArticle.UnGroupedPrice = group.ElementAt(0).Value;
                    groupedArticle.Barcode = group.ElementAt(0).Barcode;
                    groupedArticle.AcquisitionDate = group.ElementAt(0).AcquisitionDate;
                    groupedArticle.Amount = group.Sum(g => g.ArticleAmount);
                    groupedArticle.ArticleGroup = group.ElementAt(0).ArticleGroup;
                    groupedArticle.Comment = group.ElementAt(0).Comment;
                    groupedArticle.Depreciation = group.ElementAt(0).Depreciation;
                    groupedArticle.DepreciationCategory = group.ElementAt(0).DepreciationCategory;
                    groupedArticle.DepreciationTime = group.ElementAt(0).DepreciationTime;
                    groupedArticle.InsuranceCategory = group.ElementAt(0).InsuranceCategory;
                    groupedArticle.IsAvailable = group.ElementAt(0).IsAvailable;
                    groupedArticle.IsDeleted = group.ElementAt(0).IsDeleted;
                    groupedArticle.LastChangest = group.ElementAt(0).LastChangest;
                    groupedArticle.OldBarcode = group.ElementAt(0).OldBarcode;
                    groupedArticle.Room = group.ElementAt(0).Room;
                    groupedArticle.SupplierBranch = group.ElementAt(0).SupplierBranch;
                    groupedArticle.UseStoredValues = useStoredValues != null ? useStoredValues.Value : group.ElementAt(0).UseStoredValues;
                    groupedArticle.Value = group.Sum(g => g.Value);

                    result.Add(groupedArticle);
                }
            }
            return result;
        }

        #region NotUsed anymore but maybe at further developments
        //public static List<Article> GetReportItems(RadGrid rgGrid, List<Article> reportSource, Boolean HasFooter) {

        //    ArticleGridHelper gridHelper = new ArticleGridHelper();

        //    if (rgGrid.MasterTableView.FilterExpression.Any()) {

        //    } else {
        //        //Get the filter expression
        //        String filterExpression = rgGrid.MasterTableView.FilterExpression;
        //        //    String filter = null;
        //        //    String specialFilter = null;

        //        //    //HAndle speical chars
        //        //    filterExpression = filterExpression.Replace("[", "");
        //        //    filterExpression = filterExpression.Replace("]", "");
        //        //    filterExpression = filterExpression.Replace("(", "");
        //        //    filterExpression = filterExpression.Replace(")", "");
        //        //    filterExpression = filterExpression.Replace("%", "");
        //        //    filterExpression = filterExpression.Replace("'", "\"");


        //        //    if (filterExpression.Contains("AND")) {

        //        //        //Spkit the filter in Entity and NonEntity parts
        //        //        List<String> queryParts = filterExpression.Split(new string[] { "AND" }, StringSplitOptions.None).ToList();
        //        //        List<String> nonEntityParts = new List<String>();
        //        //        foreach (String nonEntityField in gridHelper.NonEntityFields) {
        //        //            List<String> matches = new List<String>(queryParts.Where(p => p.Contains(nonEntityField)));
        //        //            if (matches.Any()) {
        //        //                foreach (String match in matches) {
        //        //                    nonEntityParts.Add(match);
        //        //                    queryParts.Remove(match);
        //        //                }
        //        //            }
        //        //        }
        //        //        //Connect the parts with AND
        //        //        int counter = 0;
        //        //        while (counter <= queryParts.Count - 1) {
        //        //            filter += gridHelper.HandleSpecialArguments(queryParts[counter]);
        //        //            if (counter < queryParts.Count - 1) {
        //        //                filter += " AND ";
        //        //            }
        //        //            counter++;
        //        //        }

        //        //        if (filter.Any()) {
        //        //            reportSource = Article.GetAvailable().AsQueryable().Where(filter).ToList();
        //        //        }

        //        //        if (nonEntityParts.Any()) {
        //        //            if (!reportSource.Any()) {
        //        //                reportSource = Article.GetAvailable().ToList();
        //        //            }
        //        //            foreach (String specialQuery in nonEntityParts) {
        //        //                reportSource = gridHelper.FilterSource(reportSource, specialQuery);
        //        //            }
        //        //        }

        //        //    } else {
        //        //        foreach (String nonEntityField in gridHelper.NonEntityFields) {
        //        //            if (filterExpression.Contains(nonEntityField)) {
        //        //                specialFilter = gridHelper.HandleSpecialArguments(filterExpression);
        //        //                break;
        //        //            }
        //        //        }
        //        //        if (specialFilter != null) {
        //        //            reportSource = gridHelper.FilterSource(Article.GetAvailable().ToList(), gridHelper.HandleSpecialArguments(specialFilter));
        //        //        } else {
        //        //            reportSource = gridHelper.FilterSource(Article.GetAvailable().ToList(), gridHelper.HandleSpecialArguments(filterExpression));
        //        //        }
        //        //    }
        //        //}
        //        //if (HasFooter) {
        //        //    if (rgGrid.MasterTableView.GetItems(GridItemType.Footer) != null) {
        //        //        GridFooterItem dataItem = rgGrid.MasterTableView.GetItems(GridItemType.Footer)[0] as GridFooterItem;
        //        //        Article article = new Article();
        //        //        article.Name = "Total:";
        //        //        article.Value = double.Parse(dataItem["Value"].Text);
        //        //        article.DepreciationValue = double.Parse(dataItem["DepreciationValue"].Text);
        //        //        reportSource.Add(article);
        //        //    }
        //        reportSource = reportSource.AsQueryable().Where(filterExpression).ToList();
        //    }

        //    return reportSource;
        //}

        //public String HandleSpecialArguments(String queryPart) {
        //    if (queryPart.Contains("LIKE")) {
        //        String[] qparams = queryPart.Split("LIKE".ToCharArray());
        //        qparams[0] = qparams[0].Replace(" ", "");
        //        queryPart = queryPart.Replace(queryPart, qparams[0] + ".Contains(" + qparams[4] + ")");
        //    }
        //    if (queryPart.Contains(',')) {
        //        String[] qparams = queryPart.Split(',');
        //        queryPart = qparams[qparams.Length - 1];
        //    }
        //    if (queryPart.Contains(".ToString")) {
        //        queryPart = queryPart.Replace(".ToString", "");
        //    }
        //    return queryPart;
        //}

        //public List<Article> FilterSource(List<Article> source, String argument) {
        //    return source.AsQueryable().Where(this.HandleSpecialArguments(argument)).ToList();
        //}

        #endregion
    }
}