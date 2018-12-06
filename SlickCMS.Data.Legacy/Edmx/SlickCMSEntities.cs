using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace SlickCMS.Data.Legacy
{
    public partial class SlickCMSEntities
    {
        public static SlickCMSEntities Create()
        {
            return Create(60);
        }

        public static SlickCMSEntities Create(int commandTimeout)
        {
            var db = new SlickCMSEntities();

            // NOTE: there are 2 ways of casting here:
            var objectContext = (db as IObjectContextAdapter).ObjectContext;
            //var objectContext = ((IObjectContextAdapter)db).ObjectContext;

            objectContext.CommandTimeout = commandTimeout;

            return db;
        }
    }
}
