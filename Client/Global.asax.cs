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
using System.Security.Principal;

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

        /// <summary>
        /// This all is a bit of cheating.
        /// First the auth cookie is requested. if its not set check for the manual login required.
        /// If the manual login is required a auth ticket will be set. This allows us to access the login page even without beeing logged in through the window auth process.
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void WindowsAuthentication_OnAuthenticate(Object source, WindowsAuthenticationEventArgs e)
        {
            //if (Request.Cookies.Get(Constants.AUTHORIZATION_COOKIE_NAME) != null) {
            //    return;
            //}
            ////} else if (EntityFactory.RequiresManualLogin) {
            ////    SetUpAuthenticationTicket(null, null, Response);
            ////    return;
            ////}

            //bool loggedIn = false;
            //String strUserIdentity = e.Identity.Name;
            //AdLookup adLookup = new AdLookup();
            //String role = null;

            //if (strUserIdentity != null && strUserIdentity != "")
            //{

            //    String userDomain = AppUser.GetDomainFromDomainString(strUserIdentity);
            //    String userName = AppUser.GetUserNameFromDomainString(strUserIdentity);

            //    loggedUser = AppUser.GetByUserNameAndDomain(userName, userDomain);

            //    //If User is set, user is loggedin
            //    if (loggedUser != null && loggedUser.UserName.Length > 0 && loggedUser.IsActive && loggedUser.IsAdmin)
            //    {
            //        loggedIn = true;
            //        role = loggedUser.IsAdmin ? UserRole.Administrator.ToString() : UserRole.User.ToString();
            //        EntityFactory.Context.LoggedUser = loggedUser;
            //    }
            //}

            //////Create cookie for user if he is logged in
            //if (loggedIn)
            //{
            //    try
            //    {
            //        SetUpAuthenticationTicket(strUserIdentity, role, Response);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //}
            //else
            //{
            //    HttpContext.Current.User = null;
            //    EntityFactory.RequiresManualLogin = true;
            //    EntityFactory.Context.LoggedUser = null;
            //    Response.Redirect(Constants.AUTHORIZATION_MANUALLY_LOGIN);
            //}
        }

        public static void SetUpAuthenticationTicket(String identity, String role, HttpResponse Response) {
            FormsAuthenticationTicket formsAuthTicket = new FormsAuthenticationTicket(1, identity, DateTime.Now, DateTime.Now.AddMinutes(20), false, role);
            String strEncryptedTicket = FormsAuthentication.Encrypt(formsAuthTicket);
            HttpCookie httpCook = new HttpCookie(Constants.AUTHORIZATION_COOKIE_NAME, strEncryptedTicket);
            Response.Cookies.Add(httpCook);
        }

        public static void SetUpFormAuthenticationTicket(String identity, String role, HttpResponse Response) {
            string uname = AppUser.GetUserNameFromDomainString(identity);
            string domain = AppUser.GetDomainFromDomainString(identity);

            GenericIdentity id = new GenericIdentity(identity);

            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(
                    1,
                    identity,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(20),
                    false,
                    Guid.NewGuid().ToString());

            // Encrypt the ticket
            string encrypted_ticket = FormsAuthentication.Encrypt(ticket);

            // Create cookie
            HttpCookie cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                encrypted_ticket);

            // Add cookie
            HttpContext.Current.Response.Cookies.Add(cookie);
            EntityFactory.Context.LoggedUser = AppUser.GetByUserNameAndDomain(uname, domain);
        }
    }
}
