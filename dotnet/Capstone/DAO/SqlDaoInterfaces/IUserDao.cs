using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface IUserDao
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User CreateUser(string username, string password, string role, string emailAddress);
    }
}
