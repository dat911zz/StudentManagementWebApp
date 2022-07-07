using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementWebApp.Models
{
    public class Student
    {
        #region Properties
        /// <summary>
        /// Mã sinh viên
        /// </summary>
        //[Required(ErrorMessage = "Vui lòng nhập mã!")]
        [Display(Name = "Mã Sinh Viên")]
        public virtual string Id { get; set; }
        /// <summary>
        /// Họ tên 
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập tên!")]
        [Display(Name = "Họ Tên")]
        public virtual string Name { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập giới tính!")]
        [Display(Name = "Giới Tính")]
        public virtual string Gender { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Required(ErrorMessage = "Vui lòng chọn ngày!")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày Sinh")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//Định dạng xuất cho kiểu ngày giờ 
        public virtual DateTime DayOfBirth { get; set; }
        /// <summary>
        /// Lớp
        /// </summary>  
        [Required(ErrorMessage = "Vui lòng chọn lớp!")]
        [Display(Name = "Lớp")]
        public virtual string ClassId { get; set; }
        /// <summary>
        /// Khóa
        /// </summary>
        [Required(ErrorMessage = "Vui lòng chọn khóa!")]
        [Display(Name = "Khóa")]
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