using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IData
{
    public interface IResultData
    {
        /// <summary>
        /// Lấy thông tin học phần (môn học + điểm) từ DB cập nhật cho toàn bộ sinh viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Result> GetResultList(string id);
        void Add(string id, List<Result> rl);
        void UpdateScore(string id, string mmh, float dqt, float dtp);
    }
}
