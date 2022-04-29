using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IData
{
    public interface ICourseData
    {
        void GetAllCTHP(ref List<Student> list_sv, List<Subject> list_mh);
        void Add(Course cthp);
    }
}
