using LibraryManagementSystemAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Repositories.Abstract
{
    public interface IUserRepository
    {
        User AuthenticateUser(string username, string password);
        List<User> GetAllUsers();
        User GetUserById(int id);
        int AddUsers(User user);
        void EditUser(User user);
        void DeleteUsers(int userIds);
    }
}
