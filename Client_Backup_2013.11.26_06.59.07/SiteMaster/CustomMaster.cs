using Data.Enum;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.SiteMaster
{
    public class CustomMaster : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Gets the logged user from context and stores in session
        /// </summary>
        public AppUser User
        {
            get
            {
                //Check if session is set with user
                if (Session[SessionName.LoggedUser.ToString()] == null)
                {
                    //Check if user is set in context
                    if (EntityFactory.Context.LoggedUser != null)
                    {
                        Session[SessionName.LoggedUser.ToString()] = EntityFactory.Context.LoggedUser;
                    }
                    else
                    {
                        //EntityFactory.Context.LoggedUser = AppUser.GetByLogin(Page.User.Identity, HttpContext.Current.User.Identity.Name);
                        //Session[SessionName.LoggedUser.ToString()] = EntityFactory.Context.LoggedUser;
                    }
                }
                return Session[SessionName.LoggedUser.ToString()] as AppUser;
            }
            set {
                Session[SessionName.LoggedUser.ToString()] = value;
            }
        }

        public List<Article> ReportDataSource {
            get {
                if (Session["ReportItems"] == null) {
                    Session["ReportItems"] = new List<Article>();
                }
                return Session["ReportItems"] as List<Article>;
            }
            set {
                Session["ReportItems"] = null;
                Session["ReportItems"] = value;
            }
        }

        /// <summary>
        /// This is used for the purpose of grouping. If user groups ReportDataSource in reports he cannot undo grouping.
        /// This is why the ungrouped datasource will be stored here
        /// </summary>
        public List<Article> UngroupedReportDataSource {
            get {
                if (Session["BackupReportItems"] == null) {
                    Session["BackupReportItems"] = new List<Article>();
                }
                return Session["BackupReportItems"] as List<Article>;
            }
            set {
                Session["BackupReportItems"] = null;
                Session["BackupReportItems"] = new List<Article>(value);
            }
        } 

        public List<Article> ExportItems {
            get {
                if (Session["ExportItems"] == null) {
                    Session["ExportItems"] = new List<Article>();
                }
                return Session["ExportItems"] as List<Article>;
            }
            set {
                Session["ExportItems"] = null;
                Session["ExportItems"] = value;
            }
        }

        public StandardMaster StandardMaster {
            get {
                return Page.Master as StandardMaster;
            }
        }
    }
}