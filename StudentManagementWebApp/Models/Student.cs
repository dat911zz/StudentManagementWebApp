using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Vui lòng nhập mã!")]
        public virtual string Id { get; set; }
        /// <summary>
        /// Họ tên 
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập tên!")]
        public virtual string Name { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        [Required]
        public virtual string Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]//Định dạng xuất cho kiểu ngày giờ 
        public virtual DateTime DayOfBirth { get; set; }
        /// <summary>
        /// Lớp
        /// </summary>
        [Required]
        public virtual string ClassId { get; set; }
        /// <summary>
        /// Khóa
        /// </summary>
        [Required]
        public virtual string CourseId { get; set; }
        /// <summary>
        /// Chi tiết học phần
        /// </summary>
        public virtual Course CourseDetail { get; set; }

        #endregion
        #region Contructors
        public Student() 
        {
            this.CourseDetail = new Course();
        }
        public Student(string id, string name, string gender, DateTime dayOfBirth, string classId, string courseId)
        {
            Id = id;
            Name = name;
            Gender = gender;
            DayOfBirth = dayOfBirth;
            ClassId = classId;
            CourseId = courseId;
            CourseDetail = new Course();
        }
        public Student(Student x)
        {
            this.Id = x.Id;
            this.Name = x.Name;
            this.Gender = x.Gender;
            this.DayOfBirth = x.DayOfBirth;
            this.ClassId = x.ClassId;
            this.CourseId = x.CourseId;
            this.CourseDetail = x.CourseDetail;
        }
        #endregion
    }
}