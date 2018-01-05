using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace SlickCMS.Data
{
    public partial class SlickCMSEntities
    {
        private static int commandTimeout = 60;

        public static SlickCMSEntities Create()
        {
            var db = new SlickCMSEntities();

            // NOTE: there are 2 ways of casting here:
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            //var objectContext = ((IObjectContextAdapter)db).ObjectContext;

            objectContext.CommandTimeout = commandTimeout;

            return db;
        }

        public static SlickCMSEntities Create(int commandTimeoutOverride)
        {
            var db = new SlickCMSEntities();
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            objectContext.CommandTimeout = commandTimeout;
            return db;
        }
    }
}
