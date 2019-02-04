using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    public interface IUserService : IEntityService<User>
    {
        bool Login();
        bool IsAuthenticated();
        bool IsAuthorised();
        bool Logout();
    }
}
