using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models.Map
{
    public class SubjectMap : ClassMap<Subject>
    {
        public SubjectMap()
        {
            Id(x => x.Name);
            Map(x => x.NumOfLessons);

            Table("MonHoc");
        }
    }
}