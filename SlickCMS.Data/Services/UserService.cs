using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService() { }
        public UserService(SlickCMSContext context) : base(context) { }

        public bool Login()
        {
            // TODO
            return false;// indicate whether login was successful
        }

        public bool IsAuthenticated()
        {
            // TODO
            return false;
        }

        public bool IsAuthorised()
        {
            // TODO
            return false;
        }

        public bool Logout()
        {
            // TODO
            return false;// indicate whether logout was successful
        }
    }
}
