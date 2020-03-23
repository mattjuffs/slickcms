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

        public User Login(string email, string password)
        {
            // first check we have a user for this email address
            var user = this.Get(p => p.Email.ToLower() == email.ToLower());
            if (user == null)
                return null;

            // next verify the password matches (using 1 way hash)
            string hashedPassword = SlickCMS.Core.Hash.GenerateHash(password, SlickCMS.Core.Enums.HashType.MD5);
            if (hashedPassword != user.Password)
                return null;

            // user exists and password matches, they're logged in
            return user;
        }

        public bool IsAuthenticated(string adminLoggedIn)
        {
            // TODO: make this more secure, as currently it's just checking we have a string
            if ((adminLoggedIn + "") != "")
            {
                // https://stackoverflow.com/a/4458925/63100 - 32+ characters, depending on how it is formatted
                if (adminLoggedIn.Length >= 32)
                    return true;
            }

            return false;
        }

        public bool IsAuthorised()
        {
            // TODO: expand users & permissions by adding authorisation - users have to be authorised to see and use specific pages/functions
            return false;
        }

        public bool Logout()
        {
            // TODO: destroy traces of this user - session, cookie etc?
            return false;// indicate whether logout was successful
        }
    }
}
