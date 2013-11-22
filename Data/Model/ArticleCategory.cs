
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class ArticleCategory
    {

        public static IEnumerable<ArticleCategory> GetAll() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.ArticleCategories;
        }

        public static ArticleCategory GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.ArticleCategories.Where(c=>c.ArticleCategoryId == id).SingleOrDefault();
        }

        #region Public methods


        public void Delete()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.ArticleCategories.Remove(ctx.ArticleCategories.Where(c => c.ArticleCategoryId == this.ArticleCategoryId).SingleOrDefault());
            ctx.SaveChanges();
        }
        #endregion
    }
}
