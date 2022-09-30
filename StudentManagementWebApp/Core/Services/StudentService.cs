using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Services
{
    public class StudentService : IStudentService
    {
        private IStudentData _svData;
        public StudentService(IStudentData svData)
        {
            _svData = svData;
        }
        public Student Create(string ma, string ten, string gioitinh, DateTime ns, string lop, string Khóa)
        {
            return new Student(ma, ten, gioitinh, ns, lop, Khóa);
        }
        public void Add(Student sv)
        {
            _svData.Add(sv);
        }
        public List<Student> GetAll()
        {
            return _svData.GetAllSV();
        }

        public void Remove(string id)
        {
            _svData.Remove(id);
        }
        public void Update(Student sv)
        {
            _svData.Update(sv);
        }
    }
}
