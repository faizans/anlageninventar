using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Client.Util;
using Data.Model.Diagram;
using Telerik.Web.UI;

namespace Client.Site.Controls.UserSearchControl {
    public partial class UserSearchBox : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {

        }

        public Unit Width {
            set {
                this.rcbSearch.Width = value;
            }
        }

        public String Text {
            get {
                return this.rcbSearch.Text;
            }
            set {
                this.rcbSearch.Text = value;
            }
        }

        private int minimumInput;
        public int MinimumInput {
            set {
                this.minimumInput = value;
            }
            get {
                return this.minimumInput;
            }
        }

        public Boolean Enabled {
            set {
                this.rcbSearch.Enabled = value;
            }
        }

        protected void rcbSearch_ItemsRequested(object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs e) {
            if (e.Text != null && e.Text.Length > this.MinimumInput) {

                ((RadComboBox)sender).Items.Clear();
                
                AdLookup lookup = new AdLookup();
                List<AppUser> result = lookup.SearchAdUserByEmail(e.Text);

                if (result != null && result.Count() > 0) {
                    foreach (AppUser user in result) {
                        this.rcbSearch.Items.Add(new RadComboBoxItem(user.Email, user.AppUserId.ToString()));
                    }
                }
            }
        }

        public event EventHandler UserSearchBoxIndexChanged;
        protected void rcbSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e) {
            if (UserSearchBoxIndexChanged != null) {
                UserSearchBoxEventArgs usbEvent = new UserSearchBoxEventArgs();
                AppUser dbUser = AppUser.GetByEmail(this.rcbSearch.Text);
                if (dbUser != null) {
                    usbEvent.SelectedUser = dbUser;
                } else {
                    AdLookup lookup = new AdLookup();
                    AppUser adUser = lookup.GetAdUserByEmail(e.Text);
                    usbEvent.SelectedUser = adUser;
                }
               
                UserSearchBoxIndexChanged(this, usbEvent);
            }
        }
    }
}