using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Services
{
    public class CourseService
    {
        public CourseService()
        {

        }
        public List<Subject> GetSubjectList(List<Result> list)
        {
            List<Subject> sl = new List<Subject>();
            list.ForEach(x => sl.Add(x.SubjectDetail));
            return sl;
        }
    }
}