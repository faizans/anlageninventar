
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class AppUser
    {
        public static AppUser GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers.Where(c => c.AppUserId == id).SingleOrDefault();
        }

        public static AppUser GetByUserName(string username)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers.Where(c => c.UserName == username).SingleOrDefault();
        }

        public static IEnumerable<AppUser> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers;
        }

    }
}
