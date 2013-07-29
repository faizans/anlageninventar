using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class ArticleGroup {

        public static ArticleGroup GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.ArticleGroups.Where(c => c.ArticleGroupId == id).SingleOrDefault();
        }

        public static IEnumerable<ArticleGroup> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.ArticleGroups;
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.ArticleGroups.Remove(ctx.ArticleGroups.Where(p => p.ArticleGroupId == this.ArticleGroupId).SingleOrDefault());
        }
    }
}
