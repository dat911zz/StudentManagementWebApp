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
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Gender);
            Map(x => x.DayOfBirth);
            Map(x => x.ClassId);
            Map(x => x.Faculty);

            Table("SinhVien");
        }
    }
}