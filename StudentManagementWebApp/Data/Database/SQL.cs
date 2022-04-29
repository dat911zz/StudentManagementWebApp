using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Utilites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace StudentManagementWebApp.Data.Database
{
    /// <summary>
    /// Interacting with SQL Databse 
    /// </summary>
    public class SQL : IStudentData, ISubjectData, ICourseData
    {
        //---log test server name : DESKTOP-GUE0JS7
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlCommandBuilder builder;
        DataTable tb;
        //Khởi tạo kết nối tới CSDL
        public SqlConnection GetConnection(string datasource, string database, string username, string password)
        {
            SqlConnection conn = new SqlConnection(DatabaseHelper.GenerateConnectionString(datasource, database, username, password));
            return conn;

        }
        //Test kết nối với mẫu chuỗi kết nối
        public SqlConnection GetConnection()
        {
            string datasource = $@"";
            string database = "SinhVien";
            string username = "test01";
            string password = "1234";
            return GetConnection(datasource, database, username, password);
        }

        public DataTable SetDataSinhVien()
        {
            return SetDataTable("dataGridView1", "SELECT * FROM SinhVien");
        }
        public DataTable SetDataMonHoc()
        {
            return SetDataTable("dataGridView1", "SELECT * FROM MonHoc");
        }

        public DataTable SetDataTable(string datagridview, string query)
        {
            tb = new DataTable(datagridview);
            da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand(query, GetConnection());
            builder = new SqlCommandBuilder(da);
            da.Fill(tb);
            return tb;
        }
        public void UpdateData(DataTable tb)
        {
            if (da != null && builder != null)
            {
                da.Update(tb);
            }
        }
        //public void FillGrid(DataGridView dgv)
        //{
        //    SqlConnection conn = GetConnection();
        //    conn.Open();
        //    cmd = new SqlCommand("SELECT * FROM SinhVien", conn);
        //    DataTable tbl = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(tbl);
        //    dgv.DataSource = tbl;
        //    dgv.ReadOnly = true;
        //    conn.Close();
        //}
        public List<Student> GetAllSV()
        {
            List<Student> list = new List<Student>();
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM SinhVien", conn);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string mssv = reader.GetString(0);
                        string tensv = reader.GetString(1);
                        string gioitinh = reader.GetString(2);
                        DateTime ngaysinh = reader.GetDateTime(3);
                        string lop = reader.GetString(4);
                        string khoa = reader.GetString(5);
                        Student sv = new Student(mssv, tensv, gioitinh, ngaysinh, lop, khoa);
                        list.Add(sv);
                    }
                }
            }
            conn.Close();
            return list;
        }

        public List<Subject> GetAllMH()
        {
            List<Subject> list = new List<Subject>();
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM MonHoc", conn);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string ten = reader.GetString(0);
                        int sotiet = reader.GetInt32(1);
                        Subject sv = new Subject(ten, sotiet);
                        list.Add(sv);
                    }
                }
            }
            conn.Close();
            return list;
        }
        
        public void GetAllCTHP(ref List<Student> list_sv, List<Subject> list_mh)
        {
            List<Subject> list = new List<Subject>();
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM DKHP", conn);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                int i = 0, mh = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        List<Subject> tmp = new List<Subject>(list_mh.ToArray());
                        //-------------INPUT DATA----------------
                        for (int col = 0; col < reader.FieldCount; col++)
                        {
                            if (col > 1)
                            {
                                if (reader.GetInt32(col) == 1)
                                {
                                    //========Deep copy========= 
                                    Subject c = new Subject(tmp[mh++]);
                                    list_sv[i].CourseDetail.SubjectList.Add(new Result(new Subject(c), new Score()));
                                }
                            }
                            
                        }
                        mh = 0;
                        i++;
                    }
                }
                Console.WriteLine();
            }
            conn.Close();
        }
        public void Add(Student sv)
        {
            throw new NotImplementedException();
        }

        public void Add(Subject sv)
        {
            throw new NotImplementedException();
        }

        public void Add(Course cthp)
        {
            throw new NotImplementedException();
        }
    }
}
