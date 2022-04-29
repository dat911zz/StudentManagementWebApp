using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IServices
{
    public interface IStudentService
    {
        List<Student> GetAll();
        Student Create(string ma, string ten, string gioitinh, DateTime ns, string lop, string khoa);
        void GetInfo(Student sv);
    }
}
