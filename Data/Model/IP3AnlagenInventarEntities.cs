﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        public override int SaveChanges() {
            try {
                return base.SaveChanges();
            } catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }

                throw;  // You can also choose to handle the exception here...
            }
        }
    }
}
