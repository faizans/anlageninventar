using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.Model.Diagram;


namespace Client.Util {
    public class AdLookup {
        String ldapServerName = Constants.ldapServerName;
        String ldapUserName = Constants.ldapUserName;
        String ldapPassword = Constants.ldapPassword;

        public AppUser GetAdUserByEmail(String email) {

            String filter = "(&(objectCategory=person)(userPrincipalName=" + email + "*))";
            List<AppUser> matches = getUser(filter);
            return matches != null && matches.Count > 0 ? matches[0] : null;
        }

        public List<AppUser> SearchAdUserByEmail(String email) {

            String filter = "(&(objectCategory=person)(userPrincipalName=" + email + "*))";
            List<AppUser> matches = getUser(filter);
            return matches != null && matches.Count > 0 ? matches : null;
        }

        public AppUser GetAdUserByUsername(String username) {
            if (username != null) {
                String filter = "(&(objectCategory=person)(sAMAccountName=" + username + "*))";
                List<AppUser> matches = getUser(filter);
                return matches != null && matches.Count > 0 ? matches[0] : null;
            } else {
                return null;
            }
        }

        private List<AppUser> getUser(String filter) {
            List<AppUser> matches = new List<AppUser>();
            DirectoryEntry oRoot = new DirectoryEntry("LDAP://" + ldapServerName + "/DC=bsl,DC=lan", ldapUserName, ldapPassword);

            try {

                //DirectorySearcher oSearcher = new DirectorySearcher(oRoot, filter, new String[] { "sn", "cn", "dc", "userid", "givenName", "userPrincipalName", "sAMAccountName", "distinguishedName", "initials", "telephoneNumber" });
                DirectorySearcher oSearcher = new DirectorySearcher(oRoot, filter, new String[] { "cn", "givenName", "userPrincipalName", "sAMAccountName", "distinguishedName", "telephoneNumber", "sn" }) { SearchScope = SearchScope.Subtree };
                SearchResultCollection results = oSearcher.FindAll();
                foreach (SearchResult result in results) {
                    DirectoryEntry entry = result.GetDirectoryEntry();
                    String distinguishedName = (String)entry.Properties["distinguishedName"].Value;
                    String domainName = distinguishedName.Substring(distinguishedName.IndexOf("DC=") + "DC=".Length);
                    domainName = domainName.Substring(0, domainName.IndexOf(","));
                    String userDomainString = (String)entry.Properties["cn"].Value + " (" + domainName + ")";
                    String userDataString = domainName.ToUpper() + "\\" + ((String)entry.Properties["sAMAccountName"].Value).ToLower() + "|" + (String)entry.Properties["givenName"].Value + "|" + (String)entry.Properties["sn"].Value + "|" + (String)entry.Properties["userPrincipalName"].Value + "|" + (String)entry.Properties["telephoneNumber"].Value;

                    AppUser user = new AppUser();
                    String[] pars = userDataString.Split('|');
                    String adAccount = pars[0];
                    String username = pars[0].Split('\\')[1];
                    String domain = pars[0].Split('\\')[0];
                    String firstName = pars[1];
                    String lastName = pars[2];
                    String userEmail = pars[3];

                    user.Email = userEmail;
                    user.UserName = username;
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.Domain = domain;
                    matches.Add(user);
                }
            } catch(Exception e){}

            return matches;
        }
    }
}
