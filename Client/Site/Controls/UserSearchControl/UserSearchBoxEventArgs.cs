using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model.Diagram;

namespace Client.Site.Controls.UserSearchControl {
    public class UserSearchBoxEventArgs : EventArgs  {
        public AppUser SelectedUser;
    }
}