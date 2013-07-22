
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
            ctx.SaveChanges();
        }
        #endregion
    }
}
