using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Services
{
    /// <summary>
    /// Class to interact with Score 
    /// </summary>
    public class ScoreService
    {     
        //Ghi điểm môn học (sinh viên)
        public virtual void setScore(Score kq, double ScoreQT, double ScoreTP)
        {
            if ((ScoreQT < 0 || ScoreQT > 10) && (ScoreTP < 0 || ScoreTP > 10))
            {
                throw new Exception("Lỗi gòi: Nhập vượt ngoài phạm vi cho phép (0-10)");
            }
            kq.QT = ScoreQT;
            kq.TP = ScoreTP;
        }
        //Tính điểm tổng kết
        public virtual double finnalScore(Score d)
        {
            return (d.QT + d.TP) / 2;
        }
        //Kiểm tra sinh viên đậu hay rớt môn đã chọn
        public virtual bool isPass(Score d)
        {
            return finnalScore(d) >= 4;
        }
        //Lấy thông tin điểm
        public virtual void GetInfo(Score d)
        {
            Console.Write($"\tĐiểm thành phần: {d.TP}" + Environment.NewLine +
                $"\tĐiểm qua trình: {d.QT}" + Environment.NewLine
                );
        }
    }
}
