using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IServices
{
    public interface ICourseService
    {
        /// <summary>
        /// Lấy danh sách học phần của sinh viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Course GetCourse(string id);

        /// <summary>
        /// Thêm danh sách học phần vào DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sl"></param>
        void AddCourse(string id, List<Subject> sl);
    }
}
