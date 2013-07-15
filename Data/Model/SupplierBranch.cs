using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class SupplierBranch
    {
        public static SupplierBranch GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.SupplierBranches.Where(c => c.SupplierBranchId == id).SingleOrDefault();
        }

        public static IEnumerable<SupplierBranch> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.SupplierBranches;
        }

    }
}
