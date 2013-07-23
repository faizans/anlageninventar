
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class AppUser : IPrincipal
    {

        #region IPrincipal Members

        private IIdentity identity;
        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(String role)
        {
            throw new Exception("No implemented");
        }

        #endregion

        #region Object Methods

        public AppUser GetDbAccount()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers.Where(a => a.Email == this.Email).SingleOrDefault();
        }

        #endregion

        #region Authorization Methods

        public static AppUser GetByLogin(IIdentity identity, String login)
        {
            AppUser retUser = null;
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            if (login.IndexOf('@') > 0)
            {
                //Change to email
                retUser = ctx.AppUsers.Where(au => au.Email == login).SingleOrDefault();
            }
            else
            {
                int backslashIndex = login.IndexOf("\\");
                String domain = backslashIndex >= 0 ? login.Substring(0, backslashIndex) : "";
                String username = backslashIndex >= 0 ? login.Substring(backslashIndex + 1, login.Length - backslashIndex - 1) : login;

                retUser = ctx.AppUsers.Where(au => au.UserName == username).SingleOrDefault();
            }
            if (retUser != null)
            {
                retUser.identity = identity;
            }
            return retUser;
        }

        public static string GetUserNameFromDomainString(string domainString)
        {
            int backslashIndex = domainString.IndexOf("\\");
            String username = backslashIndex >= 0 ? domainString.Substring(backslashIndex + 1, domainString.Length - backslashIndex - 1) : domainString;
            return username;
        }

        public static string GetDomainFromDomainString(string domainString)
        {
            int backslashIndex = domainString.IndexOf("\\");
            String domain = backslashIndex >= 0 ? domainString.Substring(0,backslashIndex) : domainString;
            return domain;
        }

        #endregion

        #region Static Methods

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

        public static AppUser GetByUserNameAndDomain(string username, string domain)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers.Where(c => c.UserName.ToLower() == username.ToLower() && c.Domain.ToLower() == domain.ToLower()).SingleOrDefault();
        }

        public static AppUser GetByEmail(string email)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers.Where(c => c.Email == email).SingleOrDefault();
        }

        public static IEnumerable<AppUser> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.AppUsers;
        }

        #endregion

    }
}
