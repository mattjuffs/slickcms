using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class RelationshipService : BaseService<Relationship>, IRelationshipService
    {
        public RelationshipService() { }
        public RelationshipService(SlickCMSContext context) : base(context) { }

        // TODO
    }
}
