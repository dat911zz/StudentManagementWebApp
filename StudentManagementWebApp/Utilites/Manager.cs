using Castle.Windsor;
using StudentManagementWebApp.Data.Database;
using StudentManagementWebApp.Container;
using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Utilites
{
    public class Manager
    {
        WindsorContainer container;
        public Manager()
        {
            #region Install container
            container = new WindsorContainer();
            container.Install(new ServicesInstaller());
            container.Dispose();
            #endregion
        }
        public void AutoWork(ref List<Student> list_sv, List<Subject> list_mh)
        {
            AutoDKHP(ref list_sv, list_mh);
            AutoImportScore(list_sv);
        }
        /// <summary>
        /// Đăng ký học phần tự động
        /// </summary>
        /// <param name="list_sv"></param>
        /// <param name="list_mh"></param>
        public void AutoDKHP(ref List<Student> list_sv, List<Subject> list_mh)
        {
            ICourseData CTHP_data = container.Resolve<ICourseData>();
            CTHP_data.GetAllCTHP(ref list_sv, list_mh);          
        }
        /// <summary>
        /// Nhập điểm tự động
        /// </summary>
        /// <param name="list_sv"></param>
        public void AutoImportScore(List<Student> list_sv)
        {
            Random score1 = new Random();
            Random score2 = new Random();
            ScoreService ds = container.Resolve<ScoreService>();

            for (int i = 0; i < list_sv.Count; i++)
            {
                for (int j = 0; j < list_sv[i].CourseDetail.SubjectList.Count; j++)
                {
                    ds.setScore(list_sv[i].CourseDetail.SubjectList[j].ScoreDetail, score1.Next(0, 10), score2.Next(2, 10));
                }
            }
        }
        /// <summary>
        /// Chuyển đổi DataTable -> HTML Table
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "";
            //add header row
            html += "<thead>";
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<th>" + dt.Columns[i].ColumnName + "</th>";
            html += "</tr>";
            html += "</thead>";
            //add rows
            html += "<tbody>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</tbody>";
            html += "";
            return html;
        }
        #region Xuất console thông tin về sv
        //public void GetInfoForm(List<Student> list_sv)
        //{
        //    ResultService kqs = container.Resolve<ResultService>();
        //    foreach (var item in list_sv)
        //    {
        //        Console.WriteLine($"- Sinh Viên {item.TenSV}");
        //        for (int i = 0; i < item.CTHP.DSMH.Count; i++)
        //        {                   
        //            Console.WriteLine();
        //        }
        //    }
        //}
        //public void GetAllListInfo(List<Student> list_sv)
        //{
        //    ResultService kqs = container.Resolve<ResultService>();
        //    list_sv.ForEach(x => {
        //        Console.WriteLine($"- Sinh Viên {x.TenSV}");
        //        for (int i = 0; i < x.CTHP.DSMH.Count; i++)
        //        {
        //            kqs.GetInfoAll(x.CTHP.DSMH[i++]);
        //        }
        //    });
        //}
        //public void GetAllSubject_SV(List<Student> list_sv)
        //{
        //    ResultService kqs = container.Resolve<ResultService>();          
        //    list_sv.ForEach(x => {
        //        Console.WriteLine($"- Sinh Viên {x.TenSV}");
        //        for (int i = 0; i < x.CTHP.DSMH.Count; i++)
        //        {
        //            kqs.GetInfoSubject(x.CTHP.DSMH[i++]);
        //        }
        //    });
        //}
        #endregion
        #region Tải thông tin lên bảng ảo
        public DataTable UploadSubjectSVIntoDGV(Student sv)
        {
            int stt = 1;
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên MH", typeof(string));
            dt.Columns.Add("Số tiết", typeof(int));

            foreach (var item in sv.CourseDetail.SubjectList)
            {
                dt.Rows.Add(stt++, item.SubjectDetail.Name , item.SubjectDetail.NumOfLessons);

            }
            return dt;
        }
        public DataTable UploadScoreSVIntoDGV(Student sv)
        {
            int stt = 1;
            ScoreService ds = container.Resolve<ScoreService>();
            DataTable dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Tên MH", typeof(string));
            dt.Columns.Add("Số tiết", typeof(int));
            dt.Columns.Add("Điểm quá trình", typeof(int));
            dt.Columns.Add("Điểm thành phần", typeof(int));
            dt.Columns.Add("Kết quả", typeof(string));

            foreach (var item in sv.CourseDetail.SubjectList)
            {
                dt.Rows.Add(stt++, item.SubjectDetail.Name.ToString(), item.SubjectDetail.NumOfLessons, item.ScoreDetail.QT, item.ScoreDetail.TP, ds.isPass(item.ScoreDetail) == true ? "Đậu" : "Trượt");           
            }
            return dt;
        }
        #endregion
    }
}
