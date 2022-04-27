using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models
{
    public class Subject
    {
        #region Properties
        /// <summary>
        /// Tên môn học
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Số tiết học
        /// </summary>
        public virtual int NumOfLessons { get; set; }

        #endregion
        #region Constructors
        public Subject() { }
        public Subject(string name, int numOfLessons)
        {
            Name = name;
            NumOfLessons = numOfLessons;
        }
        public Subject(Subject x)
        {
            this.Name = x.Name;
            this.NumOfLessons = x.NumOfLessons;
        }
        #endregion
    }
}