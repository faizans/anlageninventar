using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class Article {

        #region Properties

        public double? DepreciationValue {
            get {
                if (this.Depreciation != null) {
                    int depSpan = this.Depreciation.AdditionalEndDate.Value.Year -this.Depreciation.AdditionalStartDate.Value.Year;
                    int currentSpan = DateTime.Now.Year - this.Depreciation.AdditionalStartDate.Value.Year;
                    if (currentSpan <= depSpan) {
                        return (this.Value / depSpan) * (depSpan - currentSpan);
                    }
                    return null;
                }
                return null;
            }
        }

        #endregion

        public static Article GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(c => c.ArticleId == id).SingleOrDefault();
        }

        public static IEnumerable<Article> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles;
        }

        public static IEnumerable<Article> GetDeleted() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => a.IsDeleted) ;
        }

        public static IEnumerable<Article> GetAvailable() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Articles.Where(a => !a.IsDeleted);
        }

        public Boolean IsDepreciated() {
            //throw new Exception("Not implemented yet");
            return true;
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            //TODO delete group if this was last item
            ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault().IsDeleted = true;
        }

        public void DeletePhysically() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            //TODO delete group if this was last item
            ctx.Articles.Remove(ctx.Articles.Where(p => p.ArticleId == this.ArticleId).SingleOrDefault());
        }

    }
}
