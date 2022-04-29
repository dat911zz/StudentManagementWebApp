using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IData
{
    public interface ISubjectData
    {
        List<Subject> GetAllMH();
        void Add(Subject sv);
    }
}
