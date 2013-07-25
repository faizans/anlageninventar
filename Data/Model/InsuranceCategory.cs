using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class InsuranceCategory
    {
        public static IEnumerable<InsuranceCategory> GetAll() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.InsuranceCategories;
        }

        public static InsuranceCategory GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.InsuranceCategories.Where(c=>c.InsuranceCategoryId == id).SingleOrDefault();
        }

        #region Public methods

        public Boolean HasArticles() {
            return this.Articles.Any();
        }

        public void Delete()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.InsuranceCategories.Remove(ctx.InsuranceCategories.Where(c => c.InsuranceCategoryId == this.InsuranceCategoryId).SingleOrDefault());
            ctx.SaveChanges();
        }
        #endregion
    }
}
