using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;

namespace StudentManagementWebApp.Utilites.Comparer
{
    public class SubjectEComparer : IEqualityComparer<Subject>
    {
        public bool Equals(Subject x, Subject y)
        {
            if (object.ReferenceEquals(x,y))
            {
                return true;
            }
            if (object.ReferenceEquals(x,null) || object.ReferenceEquals(y,null))
            {
                return false;
            }
            return x.SubjectId.CompareTo(y.SubjectId) == 0 && x.Name.CompareTo(y.Name) == 0;
        }

        public int GetHashCode(Subject obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int IDHashCode = obj.SubjectId.GetHashCode();
            int NameHashCode = obj.Name.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }
    }
}