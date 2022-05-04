using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models.Map
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Id(x => x.Id).Column("MaSV");  
            Map(x => x.Name).Column("TenSV");
            Map(x => x.Gender).Column("GioiTinh");
            Map(x => x.DayOfBirth).Column("NgaySinh");
            Map(x => x.ClassId).Column("Lop");
            Map(x => x.CourseId).Column("Khoa");

            Table("SinhVien");
        }
    }
}