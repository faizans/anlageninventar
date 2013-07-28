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
                if (Session[SessionName.LoggedUser.ToString()] == null)
                {
                    if (EntityFactory.Context.LoggedUser != null)
                    {
                        Session[SessionName.LoggedUser.ToString()] = EntityFactory.Context.LoggedUser;
                    }
                    else
                    {
                        EntityFactory.Context.LoggedUser = AppUser.GetByLogin(Page.User.Identity, HttpContext.Current.User.Identity.Name);
                        Session[SessionName.LoggedUser.ToString()] = EntityFactory.Context.LoggedUser;
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