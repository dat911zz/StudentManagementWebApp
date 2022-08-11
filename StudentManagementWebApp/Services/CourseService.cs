using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Services
{
    public class CourseService : ICourseService
    {
        IResultService _rsv;
        public CourseService(IResultService rsv)
        {
            _rsv = rsv;
        }
        public Course GetCourse(string id)
        {
            Course c = new Course();
            c.ResultList = new List<Result>(_rsv.GetResultList(id));
            return c;
        }
        public void AddCourse(string id, List<Subject> sl)
        {
            Course c = new Course();
            sl.ForEach(x => c.ResultList.Add(new Result(x, new Score())));
            
            _rsv.Add(id, c.ResultList);
        }
    }
}