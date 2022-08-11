using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IServices
{
    public interface IResultService
    {
        /// <summary>
        /// Lấy thông tin học phần (môn học + điểm) từ DB cập nhật cho toàn bộ sinh viên
        /// </summary>
        /// <param name="list_sv"></param>
        List<Result> GetResultList (string id);

        void Add(string id, List<Result> rl);
    }
}
