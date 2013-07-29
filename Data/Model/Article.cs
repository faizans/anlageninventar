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

        private double? depreciationValue = 0;
        /// <summary>
        /// The value of this item will only be set if depreciation is null
        /// </summary>
        public double? DepreciationValue {
            get {
                if (this.Depreciation != null) {
                    int depSpan = this.Depreciation.AdditionalEndDate.Value.Year - this.Depreciation.AdditionalStartDate.Value.Year;
                    int currentSpan = DateTime.Now.Year - this.Depreciation.AdditionalStartDate.Value.Year;
                    if (currentSpan <= depSpan) {
                        return (this.Value / depSpan) * (depSpan - currentSpan);
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

        public static IEnumerable<Article> GetDeleted() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => a.IsDeleted);
        }

        public static IEnumerable<Article> GetAvailable() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => !a.IsDeleted);
        }

        public Boolean IsDepreciated() {
            return this.DepreciationValue == 0;
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault().IsDeleted = true;
        }

        public void DeletePhysically() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;

            this.ArticleGroup.Articles.Remove(this);
            if (this.ArticleGroup != null) {
                if (this.ArticleGroup.Articles.Count == 0) {
                    this.ArticleGroup.Delete();
                }
            }

            ctx.Articles.Remove(ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault());
        }

    }
}
