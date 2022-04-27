﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models
{
    public class Student
    {
        #region Properties
        /// <summary>
        /// Mã sinh viên
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// Họ tên 
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public virtual string Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public virtual DateTime DayOfBirth { get; set; }
        /// <summary>
        /// Lớp
        /// </summary>
        public virtual string ClassId { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public virtual string Faculty { get; set; }


        #endregion
        #region Contructors
        public Student() { }
        public Student(string id, string name, string gender, DateTime dayOfBirth, string classId, string faculty)
        {
            Id = id;
            Name = name;
            Gender = gender;
            DayOfBirth = dayOfBirth;
            ClassId = classId;
            Faculty = faculty;
        }
        public Student(Student x)
        {
            this.Id = x.Id;
            this.Name = x.Name;
            this.Gender = x.Gender;
            this.DayOfBirth = x.DayOfBirth;
            this.ClassId = x.ClassId;
            this.Faculty = x.Faculty;
        }
        #endregion
    }
}