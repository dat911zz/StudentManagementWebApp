using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IServices
{
    public interface ISubjectService
    {
        List<Subject> GetAll();
        Subject Create(string tenMH, int soTiet);
    }
}
