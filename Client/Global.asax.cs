using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Client;
using Client.Util;
using Data.Model.Diagram;
using Data.Enum;
using Data.Model;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public class Global : HttpApplication
    {

        private AppUser loggedUser = null;

        void Application_Start(object sender, EventArgs e)
        {
        }

        void Application_End(object sender, EventArgs e)
        {
              //Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
             //Code that runs when an unhandled error occurs

        }

        protected void WindowsAuthentication_OnAuthenticate(Object source, WindowsAuthenticationEventArgs e)
        {

            if (Request.Cookies.Get(Constants.AUTHORIZATION_COOKIE_NAME) != null)
                return;

            bool loggedIn = false;
            String strUserIdentity = e.Identity.Name;
            AdLookup adLookup = new AdLookup();
            String role = null;

            if (strUserIdentity != null && strUserIdentity != "")
            {

                String userDomain = AppUser.GetDomainFromDomainString(strUserIdentity);
                String userName = AppUser.GetUserNameFromDomainString(strUserIdentity);

                loggedUser = AppUser.GetByUserNameAndDomain(userName, userDomain);

                //If User is set, user is loggedin
                if (loggedUser != null && loggedUser.UserName.Length > 0 && loggedUser.IsActive)
                {
                    loggedIn = true;
                    role = loggedUser.IsAdmin ? UserRole.Administrator.ToString() : UserRole.User.ToString();
                }

                EntityFactory.Context.LoggedUser = loggedUser;
            }

            ////Create cookie for user if he is logged in
            if (loggedIn)
            {
                try
                {
                    FormsAuthenticationTicket formsAuthTicket = new FormsAuthenticationTicket(1, strUserIdentity, DateTime.Now, DateTime.Now.AddMinutes(20), false, role);
                    String strEncryptedTicket = FormsAuthentication.Encrypt(formsAuthTicket);
                    HttpCookie httpCook = new HttpCookie(Constants.AUTHORIZATION_COOKIE_NAME, strEncryptedTicket);
                    Response.Cookies.Add(httpCook);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                HttpContext.Current.User = null;
            }
        }
    }
}
