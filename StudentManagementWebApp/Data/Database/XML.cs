using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentManagementWebApp.Interface.IData;

namespace StudentManagementWebApp.Data.Database
{
    //Class for XML Databse (for future)**** double ******* **** *****  
    public class XML : IStudentData, ISubjectData
    {
        public void Add(Student sv)
        {
            throw new NotImplementedException();
        }

        public void Add(Subject sv)
        {
            throw new NotImplementedException();
        }

        public void Add(Course cthp)
        {
            throw new NotImplementedException();
        }

        public void GetAllCTHP(ref List<Student> list_sv)
        {
            throw new NotImplementedException();
        }

        public List<Subject> GetAllMH()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAllSV()
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Student sv)
        {
            throw new NotImplementedException();
        }
    }
}
