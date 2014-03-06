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
            //TODO
            //Check if cookie is set
            
        }

        protected void Application_OnAuthenticateRequest(Object src, EventArgs e) {
            //HttpCookie cookie = Context.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
            //if (cookie != null) {
            //    FormsAuthenticationTicket winAuthTicket = FormsAuthentication.Decrypt(cookie.Value);
            //    string[] roles = winAuthTicket.UserData.Split(';');
            //    FormsIdentity formsId = new FormsIdentity(winAuthTicket);
            //    GenericPrincipal princ = new GenericPrincipal(formsId, roles);
            //    HttpContext.Current.User = princ;
            //    return;
            //} 
        }

        //public static void SetUpFormAuthenticationTicket(AppUser user, String role, HttpResponse Response) {
            

        //    //GenericIdentity id = new GenericIdentity(user.Identity.Name);

        //    //FormsAuthenticationTicket ticket =
        //    //    new FormsAuthenticationTicket(
        //    //        1,
        //    //        user.Identity.Name,
        //    //        DateTime.Now,
        //    //        DateTime.Now.AddMinutes(20),
        //    //        false,
        //    //        Guid.NewGuid().ToString());

        //    //// Encrypt the ticket
        //    //string encrypted_ticket = FormsAuthentication.Encrypt(ticket);

        //    //// Create cookie
        //    //HttpCookie cookie = new HttpCookie(
        //    //    FormsAuthentication.FormsCookieName,
        //    //    encrypted_ticket);

        //    //// Add cookie
        //    //Response.Cookies.Add(cookie);
        //    //HttpContext.Current.User = user;
        //}
    }
}
