using Client.SiteMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public CustomMaster SiteMaster
        {
            get
            {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }
    }
}