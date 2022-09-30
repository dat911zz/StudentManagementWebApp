using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Services
{
    public class UsersService : IUsersService
    {
        private IUsersData _usersData;
        public UsersService(IUsersData usersData)
        {
            _usersData = usersData;
        }

        public void Add(User user)
        {
            _usersData.Add(user);
        }

        public User Create(string firstname, string lastname, string email, string username, string hash, string roleid)
        {
            return new User(firstname, lastname, email, username, hash, roleid);
        }
        public List<User> GetAll()
        {
            return _usersData.GetAllUsers();
        }
    }
}