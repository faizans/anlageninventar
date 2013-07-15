using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class IP3AnlagenInventarEntities
    {
        private AppUser loggedUser;

        /// <summary>
        /// Will contain user information, available at any time. Filled in by YambContextFactory class methods
        /// </summary>
        public AppUser LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; }
        }
    }
}
