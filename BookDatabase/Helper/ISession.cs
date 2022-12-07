using BookDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Helper
{
    public interface ISession
    {
        void CreateUserSession(UserModel user);
        void RemoveUserSession();
        UserModel GetUserSession();
    }
}
