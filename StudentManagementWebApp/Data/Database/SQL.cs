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
    public class SQL : IStudentData, ISubjectData, ICourseData, IUsersData
    {
        //---log test server name : DESKTOP-GUE0JS7
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlCommandBuilder builder;
        DataTable tb;
        //Khởi tạo kết nối tới CSDL
        public SqlConnection GetConnection(string datasource, string database, string username, string password)
        {
            SqlConnection conn1 = new SqlConnection(DatabaseHelper.GenerateConnectionString(datasource, database, username, password));
            SqlConnection conn2 = new SqlConnection("workstation id=DBSinhVien.mssql.somee.com;packet size=4096;user id=ADMIN911;pwd=Vungodat123;data source=DBSinhVien.mssql.somee.com;persist security info=False;initial catalog=DBSinhVien");
            //Cần kiểm soát quyền truy cập vào CSDL
            //Nếu để như vầy sẽ bị lộ tài khoản
            //Tham khảo tại đây: https://viblo.asia/p/bai-toan-phan-quyen-van-de-muon-thuo-1VgZvw9mlAw
            return conn2;

        }
        //Test kết nối với mẫu chuỗi kết nối
        /// <summary>
        /// Local DB dat911zz_SQLLogin_1
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            string datasource = $@"";
            string database = "SinhVien";
            string username = "test01";
            string password = "1234";          
            return GetConnection(datasource, database, username, password);
        }
        public DataTable SetDataStudent()
        {
            return SetDataTable("dataGridView1", "SELECT * FROM SinhVien");
        }
        public DataTable SetDataSubject()
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
        //    cmd = new SqlCommand("SELECT * FROM Student", conn);
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
                        string Khoa = reader.GetString(5);
                        Student sv = new Student(mssv, tensv, gioitinh, ngaysinh, lop, Khoa);
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
        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand("SELECT * FROM Users", conn);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User(
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetBoolean(6)
                            );
                        list.Add(user);
                    }
                }
            }
            conn.Close();
            return list;
        }
        public void Add(Student sv)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand(@"INSERT INTO SinhVien VALUES (@msv, @ht, @gt, @ns, @lop, @khoa);", conn);
            #region Using Parameter to prevent SQL Injection

            cmd.Parameters.AddWithValue("@msv", sv.Id);
            cmd.Parameters.AddWithValue("@ht", sv.Name);
            cmd.Parameters.AddWithValue("@gt", sv.Gender);
            cmd.Parameters.AddWithValue("@ns", sv.DayOfBirth);
            cmd.Parameters.AddWithValue("@lop", sv.ClassId);
            cmd.Parameters.AddWithValue("@khoa", sv.CourseId);

            #endregion
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();

        }
        public void Add(Subject s)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand(@"INSERT INTO MonHoc VALUES (@ten, @st);", conn);
            #region Using Parameter to prevent SQL Injection

            cmd.Parameters.AddWithValue("@ten", s.Name);
            cmd.Parameters.AddWithValue("@st", s.NumOfLessons);

            #endregion
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void Add(Course cthp)
        {
            throw new NotImplementedException();
        }
        public void Add(User user)
        {
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand(@"INSERT INTO Users VALUES (@fname, @lname, @email, @username, @hash, 0);", conn);
            #region Using Parameter to prevent SQL Injection

            cmd.Parameters.AddWithValue("@fname", user.FirstName);
            cmd.Parameters.AddWithValue("@lname", user.LastName);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@hash", user.Hash);

            #endregion
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Remove(string id)
        {           
            SqlConnection conn = GetConnection();
            conn.Open();
            cmd = new SqlCommand($@"DELETE SinhVien WHERE MaSV = '{id}'");

            conn.Close();
        }
    }
}
