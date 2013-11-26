using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram {
    public partial class Article {

        #region Properties

        public Boolean UseStoredValues { get; set; }

        public int? ArticleAmount {
            get {
                if (this.Amount == null || this.Amount.Value <= 1) {
                    return 1;
                }
                return this.Amount;
            }
        }

        private DateTime depreciationTime = DateTime.Now;
        /// <summary>
        /// Set the year for the depreciation calculation
        /// </summary>
        public DateTime DepreciationTime {
            get {
                return this.depreciationTime;
            }
            set {
                this.depreciationTime = value;
            }
        }

        private double? depreciationValue = 0;
        /// <summary>
        /// Returns the current value of the article
        /// </summary>
        public double? DepreciationValue {
            get {

                if (this.UseStoredValues) {
                    return this.depreciationValue;
                }

                if (this.DepreciationCategory != null && this.DepreciationCategory.DepreciationSpan > 0) {
                    if (DepreciationTime.Year < this.AcquisitionDate.Value.Year) {
                        return depreciationValue = this.Value;
                    }

                    int currentSpan = (DepreciationTime.Year - this.AcquisitionDate.Value.Year) + 1;
                    int depSpan = int.Parse(this.DepreciationCategory.DepreciationSpan.ToString());

                    if (currentSpan <= depSpan) {
                        depreciationValue = this.Value - ((this.Value / depSpan) * (currentSpan));
                    } else {
                        depreciationValue = 0;
                    }
                } else {
                    depreciationValue = this.Value;
                }
                return depreciationValue;
            }
            set {
                this.depreciationValue = value;
            }
        }

        private double? averageDepreciation;
        public double? AverageDepreciation {
            get {

                if (this.UseStoredValues) {
                    return averageDepreciation;
                }

                if (this.DepreciationCategory != null && this.DepreciationCategory.DepreciationSpan > 0 && (this.AcquisitionDate.Value.Year + this.DepreciationCategory.DepreciationSpan) > this.DepreciationTime.Year) {
                    averageDepreciation = (this.Value / this.DepreciationCategory.DepreciationSpan);
                } else {
                    averageDepreciation = 0;
                }
                return averageDepreciation;
            }
        }

        private double? cumulatedDepreciation;
        public double? CumulatedDepreciation {
            get {
                if (this.UseStoredValues) {
                    return this.cumulatedDepreciation;
                }
                cumulatedDepreciation = this.Value - this.DepreciationValue;
                return cumulatedDepreciation;
            }
        }


        #region WorkAround for Telerik reporting
        //Telerik reporting grouping doesnt work on entity types (failed to compare items because of item type proxy .kjsflasjfkjsakldfj)
        public String BuildingName {
            get {
                return this.Room.Floor.Building.Name;
            }
        }
        public String FloorName {
            get {
                return this.Room.Floor.Name;
            }
        }
        public String RoomName {
            get {
                return this.Room.Name;
            }
        }
        #endregion

        #endregion

        public static Article GetById(int id) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(c => c.ArticleId == id).SingleOrDefault();
        }

        public static IEnumerable<Article> GetAll() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles
                .Include("Room")
                .Include("Room.Floor")
                .Include("Room.Floor.Building")
                .Include("Room.AppUsers")
                .Include("SupplierBranch")
                .Include("SupplierBranch.Supplier")
                .Include("InsuranceCategory")
                .Include("DepreciationCategory")
                ;
        }

        public static List<Article> GetAllSortedByUsers() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            var sortedList = Article.GetAll().Where(a => !a.IsDeleted && a.IsAvailable.Value).OrderBy(a => a.Room.ResponsiblePerson).OrderBy(a => a.Room.Name);
            return sortedList.ToList();
        }

        public static IEnumerable<Article> GetDeleted() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            var sortedList = GetAll().Where(a => a.IsDeleted).OrderBy(a => a.Name) ;
            return sortedList;
        }

        public static IEnumerable<Article> GetLost() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            var sortedList = Article.GetAll().Where(a => (a.IsAvailable.HasValue && !a.IsAvailable.Value)).OrderBy(a => a.Name);
            return sortedList;
        }

        public static IEnumerable<Article> GetLostOrDeleted() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return Article.GetAll().Where(a => (a.IsAvailable.HasValue && !a.IsAvailable.Value) || (a.IsDeleted)).OrderBy(a => a.Name);
        }


        /// <summary>
        /// Get all deleted articles with a rest value
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Article> GetDeletedWithRestValue() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            List<Article> articles = Article.GetAll().Where(a => a.IsDeleted).ToList();
            articles = articles.Where(a => !a.IsDepreciated() && a.IsAvailable.Value).OrderBy(a=>a.Name).ToList();
            return articles;
        }

        /// <summary>
        /// Get all articles which are not deleted
        /// </summary>
        /// <returns></returns>
        public static List<Article> GetAllNotDeleted() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            var result = Article.GetAll().Where(a => !a.IsDeleted).OrderBy(a => a.Name).ToList();
            return result;
        }

        /// <summary>
        /// Get all articles which are available
        /// </summary>
        /// <returns></returns>
        public static List<Article> GetAvailable() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return Article.GetAll().Where(a => !a.IsDeleted && a.IsAvailable.Value).OrderBy(a => a.Name).ToList();
        }

        public static List<Article> GetEntrances(int year) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return Article.GetAll().Where(a => (a.AcquisitionDate.HasValue && a.AcquisitionDate.Value.Year == year)).OrderBy(a => a.Name).ToList();
        }

        public static List<Article> GetDisposals(int year) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return Article.GetAll().Where(a => (a.LastChangest.HasValue && a.LastChangest.Value.Year == year)).OrderBy(a => a.Name).ToList();
        }

        public Boolean IsDepreciated() {
            if (this.DepreciationCategory != null && this.DepreciationCategory.DepreciationSpan > 0) {
                return this.DepreciationValue == 0;
            }
            return false;
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            Article article = ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault();
            article.IsDeleted = true;
            article.LastChangest = DateTime.Now;
        }

        /// <summary>
        /// Delete the article and its group
        /// </summary>
        public void DeletePhysically() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;

            if (this.ArticleGroup != null) {
                this.ArticleGroup.Articles.Remove(this);

                if (this.ArticleGroup.Articles.Count == 0) {
                    this.ArticleGroup.Delete();
                }
            }

            ctx.Articles.Remove(ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault());
        }

    }
}
