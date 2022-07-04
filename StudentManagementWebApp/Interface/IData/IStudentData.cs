using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IData
{
    public interface IStudentData
    {
        List<Student> GetAllSV();
        void Add(Student sv);
        void Remove(string id);
    }
}
