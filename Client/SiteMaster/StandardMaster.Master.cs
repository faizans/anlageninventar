using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client.SiteMaster
{
    public partial class StandardMaster : CustomMaster
    {

        protected void Page_Load(object sender, EventArgs e)
        {
	        if (this.User != null)
	        {
		        InfoUser = this.User.FirstName + " " + this.User.LastName;
	        }
	        else
	        {
		        InfoUser = "Anonymous";
	        }
        }

        public String InfoText {
            set {
                this.lblInfoText.Text = value;
            }
        }

	    public String InfoUser
	    {
		    set { this.lblInfoUser.Text = value; }
	    }
    }
}