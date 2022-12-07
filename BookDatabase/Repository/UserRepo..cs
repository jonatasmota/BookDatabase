using BookDatabase.Data;
using BookDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _databaseContext;
        public UserRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public UserModel FindByUsername(string username)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Username.ToUpper() == username.ToUpper());
        }

        public UserModel FindById(int id)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<UserModel> FindAll()
        {
            return _databaseContext.Users.ToList();
        }

        public UserModel Add(UserModel user)
        {
            // Record to Database
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();

            return user;
        }

        public UserModel Update(UserModel user)
        {
            UserModel userDb = FindById(user.Id);

            if (userDb == null) throw new Exception("Error updating user.");

            userDb.Name = user.Name;
            userDb.Email = user.Email;
            userDb.Username = user.Username;
            /*userDb.Password = user.Password;*/
            

            _databaseContext.Users.Update(userDb);
            _databaseContext.SaveChanges();

            return userDb;
        }

        public bool Delete(int id)
        {
            UserModel userDb = FindById(id);

            if (userDb == null) throw new Exception("Error deleting book.");

            _databaseContext.Users.Remove(userDb);
            _databaseContext.SaveChanges();

            return true;
        }

    }
}
