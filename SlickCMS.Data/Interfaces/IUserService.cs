using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    public interface IUserService : IEntityService<User>
    {
        bool Login(string email, string password);
        bool IsAuthenticated(string adminLoggedIn);
        bool IsAuthorised();
        bool Logout();
    }
}
