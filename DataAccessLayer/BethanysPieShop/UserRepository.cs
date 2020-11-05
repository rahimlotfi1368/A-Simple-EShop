using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace DataAccessLayer.BethanysPieShop
{
    public class UserRepository:Repository<Models.User>,IUserRepository
    {
        public UserRepository(Models.DataBaseContext dataBaseContext):base(dataBaseContext)
        {

        }

        public Models.User GetUserById(Guid Id)
        {
            var theUser = DataBaseContext.Users
                        .Where(current => current.Id == Id)
                        .Include(current => current.Role)
                        .FirstOrDefault();

            return theUser;
        }

        public User GetUserByUsername(string userName)
        {
            var theUser = DataBaseContext.Users
                        .Where(current => current.Username == userName)
                        .Include(current => current.Role)
                        .FirstOrDefault();

            return theUser;
        }
    }
}
