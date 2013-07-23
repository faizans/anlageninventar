using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class Supplier
    {
        public static Supplier GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Suppliers.Where(c => c.SupplierId == id).SingleOrDefault();
        }

        public static IEnumerable<Supplier> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Suppliers;
        }

        public Boolean HasArticles() {
            if (this.SupplierBranches.Any()) {
                foreach (SupplierBranch branch in this.SupplierBranches) {
                    if (branch.HasArticles()) {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            if (this.SupplierBranches.Any()) {
                List<SupplierBranch> supplierBranches = new List<SupplierBranch>(this.SupplierBranches);
                foreach (SupplierBranch supplierBranch in supplierBranches) {
                    supplierBranch.Delete();
                }
            }
            ctx.Suppliers.Remove(ctx.Suppliers.Where(p => p.SupplierId == this.SupplierId).SingleOrDefault());
        }

    }
}
