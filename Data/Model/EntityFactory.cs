using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class EntityFactory
    {
        private static IP3AnlagenInventarEntities context;

        public static IP3AnlagenInventarEntities Context
        {
            get
            {
                if (context == null)
                {
                    context = new IP3AnlagenInventarEntities();
                    try
                    {
                        context.Database.Exists();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Database not available!", ex);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("F*** that s***...", ex);
                    }
                }

                return context;
            }
            set { }
        }

    }
}
