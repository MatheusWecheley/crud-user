using CRUD_USERS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_USERS.Repositories
{
    public interface IUserRepository
    {
        void add(UserModel userModel);
        void update(UserModel userModel);
        void delete(int id);
        IEnumerable<UserModel> getUser(string value);

        IEnumerable<UserModel> GetAllUsers();
    }
}
