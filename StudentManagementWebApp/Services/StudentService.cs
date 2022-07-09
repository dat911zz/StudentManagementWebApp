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
        //Lấy thông tin Sinh Viên
        public void GetInfo(Student sv)
        {           
            Console.Write($"\tMSSV: {sv.Id}" + Environment.NewLine +
                $"\tHọ tên: {sv.Name}" + Environment.NewLine +
                $"\tGiới tính: {sv.Gender}" + Environment.NewLine +
                $"\tNgày sinh: {sv.DayOfBirth.ToShortDateString()}" + Environment.NewLine +
                $"\tLớp: {sv.ClassId}" + Environment.NewLine +
                $"\tKhóa: {sv.CourseId}" + Environment.NewLine
                );
        }
        //Auto DKHP cho Sinh Viên
        public void AutoImportCTHP(Student sv, string tenMH, int soTiet, double ScoreQT, double ScoreTP)
        {
            ResultService kqs = new ResultService();
            sv.CourseDetail.SubjectList.Add(new Result(new Subject(tenMH, soTiet), new Score(ScoreQT, ScoreTP)));
            foreach (var item in sv.CourseDetail.SubjectList)
            {
                kqs.GetInfoAll(item);
            }
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
