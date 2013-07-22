
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class DepreciationCategory
    {
        public static DepreciationCategory GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.DepreciationCategories.Where(c => c.DepreciationCategoryId == id).SingleOrDefault();
        }

        #region Public methods
        public void Delete()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.DepreciationCategories.Remove(ctx.DepreciationCategories.Where(c => c.DepreciationCategoryId == this.DepreciationCategoryId).SingleOrDefault());
            ctx.SaveChanges();
        }
        #endregion
    }
}
