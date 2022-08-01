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
        public virtual List<Result> ResultList { get; set; }

        #endregion
        #region Constructors
        public Course(List<Result> sl)
        {
            ResultList = sl;
        }
        public Course()
        {
            ResultList = new List<Result>();
        }
        #endregion
    }
}