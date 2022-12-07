using BookDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Repository
{
    public interface IUserRepo
    {
        UserModel FindByUsername(string username);
        UserModel FindById(int id);
        List<UserModel> FindAll();
        UserModel Add(UserModel user);
        UserModel Update(UserModel user);
        bool Delete(int id);
    }
}
