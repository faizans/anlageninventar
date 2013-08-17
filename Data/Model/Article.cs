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
        /// The value of this item will only be set if depreciation is null
        /// </summary>
        public double? DepreciationValue {
            get {
                if (this.Depreciation != null) {
                    int depSpan = this.Depreciation.AdditionalEndDate.Value.Year - this.Depreciation.AdditionalStartDate.Value.Year;
                    int currentSpan = DepreciationTime.Year - this.Depreciation.AdditionalStartDate.Value.Year > 0 ?  DepreciationTime.Year - this.Depreciation.AdditionalStartDate.Value.Year : 1;
                    if (currentSpan <= depSpan) {
                        return (this.Value / depSpan) * (currentSpan);
                    }
                    return null;
                }
                return depreciationValue;
            }
            set {
                if(this.Depreciation == null)
                    this.depreciationValue = value;
            }
        }

        #endregion

        public static Article GetById(int id) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(c => c.ArticleId == id).SingleOrDefault();
        }

        public static IEnumerable<Article> GetAll() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles;
        }

        public static List<Article> GetAllSortedByUsers() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            var sortedList = ctx.Articles.Where(a=> ! (a.IsDeleted && a.Room != null)).OrderBy(a=>a.Room.ResponsiblePerson);
            return sortedList.ToList();
        }

        public static IEnumerable<Article> GetDeleted() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => a.IsDeleted);
        }

        public static IEnumerable<Article> GetLost() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => (a.IsAvailable.HasValue && !a.IsAvailable.Value));
        }


        /// <summary>
        /// Get all deleted articles with a rest value
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Article> GetDeletedWithRestValue() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => a.IsDeleted && !a.IsDepreciated());
        } 

        /// <summary>
        /// Get all articles which are not deleted
        /// </summary>
        /// <returns></returns>
        public static List<Article> GetAvailable() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => !a.IsDeleted).ToList();
        }

        public Boolean IsDepreciated() {
            return this.DepreciationValue == 0;
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault().IsDeleted = true;
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
