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
        //public void AutoWork(ref List<Student> list_sv)
        //{
        //    AutoDKHP(ref list_sv);
        //    //AutoImportScore(list_sv);
        //}
        ///// <summary>
        ///// Đăng ký học phần tự động
        ///// </summary>
        ///// <param name="list_sv"></param>
        //public void AutoDKHP(ref List<Student> list_sv)
        //{
        //    ICourseData CTHP_data = container.Resolve<ICourseData>();
        //    CTHP_data.GetAllCTHP(ref list_sv);
        //}
        /// <summary>
        /// Nhập điểm tự động
        /// </summary>
        /// <param name="list_sv"></param>
        //public void AutoImport
        //    (List<Student> list_sv)
        //{
        //    Random score1 = new Random();
        //    Random score2 = new Random();
        //    ScoreService ds = container.Resolve<ScoreService>();

        //    for (int i = 0; i < list_sv.Count; i++)
        //    {
        //        for (int j = 0; j < list_sv[i].CourseDetail.ResultList.Count; j++)
        //        {
        //            ds.setScore(list_sv[i].CourseDetail.ResultList[j].ScoreDetail, score1.Next(0, 10), score2.Next(2, 10));
        //        }
        //    }
        //}

        /// <summary>
        /// Trích xuất danh sách môn học từ ds kết quả
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Subject> GetSubjectList(List<Result> list)
        {
            List<Subject> sl = new List<Subject>();
            list.ForEach(x => sl.Add(x.SubjectDetail));
            return sl;
        }
        public static string ConvertSubjecttListToSelectTag(List<Subject> list)
        {
            /*     
             <!--<select name="DS1" id="" class="w-100">-->
                    <!--<optgroup label="Danh sách số">
                        <option value="1">Danh sách 1</option>
                        <option value="2">Danh sách 2</option>
                        <option value="3">Danh sách 3</option>
                        <option value="4">Danh sách 4</option>
                    </optgroup>
                </select>-->         
             */
            string html = "";
            html += @"<select id='subjectList' class='w-100' placeholder='Chọn môn học'>";
            list.ForEach(x => {
                html += "<option value ='" + x.SubjectId + "'>" + x.Name + "</option>" ;
            });
            html += "</select>";
            return html;
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
                html += "<th style='color: #000'>" + dt.Columns[i].ColumnName + "</th>";
            html += "</tr>";
            html += "</thead>";
            //add rows
            html += "<tbody>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    var value = dt.Rows[i][j].ToString();
                    if (value == "Trượt")
                    {
                        html += "<td style='color: red'>" + value + "</td>";
                    }
                    else
                    {
                        double tmp = 0;
                        if (double.TryParse(value, out tmp) && tmp < 5)
                        {
                            html += "<td style='color: red'>" + value + "</td>";                    
                        }
                        else
                        {
                            html += "<td style='color: #000'>" + value + "</td>";
                        }
                    }
                }
                html += "</tr>";
            }
            html += "</tbody>";
            if (dt.Rows.Count == 0)
            {
                html += "<p style='font-size: 30px'>Không có thông tin!</p>";
            }
            html += "";
            return html;
        }

        #region Tải thông tin lên bảng ảo
        public DataTable UploadSubjectSVIntoDataTable(Student sv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Tên MH", typeof(string));
            dt.Columns.Add("Số tiết", typeof(int));

            foreach (var item in sv.CourseDetail.ResultList)
            {
                dt.Rows.Add(item.SubjectDetail.Name , item.SubjectDetail.NumOfLessons);

            }
            return dt;
        }
        public DataTable UploadScoreSVIntoDataTable(Student sv)
        {
            ScoreService ds = container.Resolve<ScoreService>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Tên MH", typeof(string));
            dt.Columns.Add("Số tiết", typeof(int));
            dt.Columns.Add("Điểm quá trình", typeof(int));
            dt.Columns.Add("Điểm thành phần", typeof(int));
            dt.Columns.Add("Kết quả", typeof(string));

            foreach (var item in sv.CourseDetail.ResultList)
            {
                dt.Rows.Add(item.SubjectDetail.Name.ToString(), item.SubjectDetail.NumOfLessons, item.ScoreDetail.QT, item.ScoreDetail.TP, ds.isPass(item.ScoreDetail) == true ? "Đậu" : "Trượt");           
            }
            return dt;
        }
        #endregion
    }
}
