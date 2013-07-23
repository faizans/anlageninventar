
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

        public static IEnumerable<DepreciationCategory> GetAll() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.DepreciationCategories;
        }

        public static DepreciationCategory GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.DepreciationCategories.Where(c => c.DepreciationCategoryId == id).SingleOrDefault();
        }

        #region Public methods

        public Boolean HasArticles() {
            if (this.Depreciations.Any()) {
                foreach (Depreciation depreciation in this.Depreciations) {
                    if (depreciation.HasArticles()) {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Delete()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            if (this.Depreciations.Any()) {
                List<Depreciation> depreciations = new List<Depreciation>(this.Depreciations);
                foreach (Depreciation depreciation in depreciations) {
                    depreciation.Delete();
                }
            }
            ctx.DepreciationCategories.Remove(ctx.DepreciationCategories.Where(p => p.DepreciationCategoryId == this.DepreciationCategoryId).SingleOrDefault());
        }
        #endregion
    }
}
