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
    /// <summary>
    /// Class for interact with Subject
    /// </summary>
    public class SubjectService : ISubjectService
    {
        private ISubjectData _mhData;
        public SubjectService(ISubjectData mhData)
        {
            _mhData = mhData;
        }

        //Ghi dữ liệu của môn học
        public virtual void setData(Subject mh, string TenMH, int SoTiet)
        {
            mh.Name = TenMH;
            mh.NumOfLessons = SoTiet;
        }
        public Subject Create(string maMH, string tenMH, int soTiet)
        {
            return new Subject(maMH, tenMH, soTiet);
        }
        public List<Subject> GetAll()
        {
            return _mhData.GetAllMH();
        }      
    }
}
