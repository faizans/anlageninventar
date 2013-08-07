
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class Depreciation
    {

        public String Name {
            get {
                if (this.AdditionalStartDate.HasValue && this.AdditionalEndDate.HasValue) {
                    return this.AdditionalStartDate.Value.Year + " - " + this.AdditionalEndDate.Value.Year;
                }
                return null;
            }
        }

        public static Depreciation GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Depreciations.Where(c => c.DepreciationId == id).SingleOrDefault();
        }

        #region Public methods

        public void Delete()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.Depreciations.Remove(ctx.Depreciations.Where(c => c.DepreciationId == this.DepreciationId).SingleOrDefault());
        }

        public Boolean HasArticles() {
            return this.Articles.Any();
        }

        #endregion
    }
}
