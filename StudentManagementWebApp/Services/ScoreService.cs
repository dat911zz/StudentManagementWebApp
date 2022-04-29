using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Services
{
    /// <summary>
    /// Class to interact with Diem 
    /// </summary>
    public class ScoreService
    {     
        //Ghi điểm môn học (sinh viên)
        public virtual void setScore(Score kq, double DiemQT, double DiemTP)
        {
            if ((DiemQT < 0 || DiemQT > 10) && (DiemTP < 0 || DiemTP > 10))
            {
                throw new Exception("Lỗi gòi: Nhập vượt ngoài phạm vi cho phép (0-10)");
            }
            kq.QT = DiemQT;
            kq.TP = DiemTP;
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
