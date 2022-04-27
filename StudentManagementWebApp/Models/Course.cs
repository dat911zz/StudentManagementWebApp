using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models
{
    public class Course
    {
        #region Properties
        /// <summary>
        /// Danh sách môn học sinh viên đã đăng ký
        /// </summary>
        public virtual List<Result> SubjectList { get; set; }

        #endregion
        #region Constructors
        public Course(List<Result> sl)
        {
            SubjectList = sl;
        }
        public Course()
        {
            SubjectList = new List<Result>();
        }
        #endregion
    }
}