using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class SupplierBranch
    {
        public String Name {
            get {
                return this.ZipCode + " - " + this.Place;
            }
        }

        public static SupplierBranch GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.SupplierBranches.Where(c => c.SupplierBranchId == id).SingleOrDefault();
        }

        public static SupplierBranch GetByPlaceAndZipCode(string place, string zipcode, int supplierId)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.SupplierBranches.Where(c => c.Place == place && c.ZipCode == zipcode && c.SupplierId == supplierId).SingleOrDefault();
        }

        public static IEnumerable<SupplierBranch> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.SupplierBranches;
        }

        #region Public methods

        public Boolean HasArticles() {
            return this.Articles.Any();
        }

        public void Delete() {
            EntityFactory.Context.SupplierBranches.Remove(EntityFactory.Context.SupplierBranches.Where(s => s.SupplierBranchId == this.SupplierBranchId).SingleOrDefault());
        }

        #endregion

    }
}
