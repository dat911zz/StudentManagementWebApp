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
        Student Create(string ma, string ten, string gioitinh, DateTime ns, string lop, string Khóa);
        void GetInfo(Student sv);
        void Add(Student sv);
        void Remove(string id);
        void Update(Student sv);
    }
}
