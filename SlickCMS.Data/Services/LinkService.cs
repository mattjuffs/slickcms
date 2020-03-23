using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class LinkService : BaseService<Link>, ILinkService
    {
        public LinkService() { }
        public LinkService(SlickCMSContext context) : base(context) { }

        // TODO
    }
}
