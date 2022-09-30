using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IServices
{
    public interface IUsersService
    {
        List<User> GetAll();
        User Create(string firstname, string lastname, string email, string username, string hash, string roleid);
        void Add(User user);
    }
}
