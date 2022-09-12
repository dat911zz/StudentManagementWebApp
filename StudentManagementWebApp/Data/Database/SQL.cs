using NLog;
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
    public class SQL : IStudentData, ISubjectData, IUsersData, IResultData
    {
        //---log test server name : DESKTOP-GUE0JS7
        private SqlCommand cmd;
        SqlDataAdapter da;
        SqlCommandBuilder builder;
        DataTable tb;
        public SQL()
        {
            
        }
        //Khởi tạo kết nối tới CSDL
        public SqlConnection GetConnection(string datasource, string database, string username, string password)
        {
            //Local DB for testing
            SqlConnection conn1 = new SqlConnection(DatabaseHelper.GenerateConnectionString(datasource, database, username, password));
            //BD on Deployment enviroment
            SqlConnection conn2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnstr"].ConnectionString);//Protect ConnectionString
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
        public List<Student> GetAllSV()
        {
            List<Student> list = new List<Student>();            
            using (SqlConnection conn = GetConnection())
            {
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
            }
            return list;
        }
        public List<Subject> GetAllMH()
        {
            List<Subject> list = new List<Subject>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM MonHoc", conn);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string sid = reader.GetString(0);
                            string name = reader.GetString(1);
                            int sotiet = reader.GetInt32(2);
                            Subject sv = new Subject(sid, name, sotiet);
                            list.Add(sv);
                        }
                    }
                }
            }
            return list;
        }     
        /// <summary>
        /// Lấy thông tin học phần (môn học + điểm) từ DB cập nhật cho toàn bộ sinh viên
        /// </summary>
        /// <param name="sv"></param>
        public List<Result> GetResultList(string id)
        {
            List<Result> rsl = new List<Result>();
            using (SqlConnection conn = GetConnection())//Để Close connection đúng cách nên xài using ;v
            {
                conn.Open();
                cmd = new SqlCommand(@"SELECT * FROM DS_CTHP_SV WHERE MaSV = @MSSV", conn);
                cmd.Parameters.AddWithValue("@MSSV", id);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rsl.Add(
                                new Result(
                                    new Subject(reader.GetString(1), reader.GetString(2), reader.GetInt32(3)),
                                    new Score(reader.GetDouble(4), reader.GetDouble(5))
                                    )
                                );
                        }
                    }
                }
            }
            return rsl;
        }
        public List<User> GetAllUsers()
        {
            List<User> list = new List<User>();
            
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM SVUser", conn);
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
                                reader.GetString(6)
                                );
                            list.Add(user);
                        }
                    }
                }
            }
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
            cmd = new SqlCommand(@"INSERT INTO SVUser VALUES (@fname, @lname, @email, @username, @hash);", conn);
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
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                cmd = new SqlCommand($@"exec fk_delete_mssv '{id}'", conn);
                int rowsAffected = cmd.ExecuteNonQuery();
            } 
        }

        public void Update(Student sv)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                cmd = new SqlCommand($@" 
                SET DATEFORMAT DMY
                update SinhVien
                set TenSV = @name, GioiTinh = @gender, NgaySinh = @date, Lop = @classId, Khoa = @courseId
                where MaSV = @id
                ", conn);

                #region Using Parameter to prevent SQL Injection

                cmd.Parameters.AddWithValue("@name", sv.Name);
                cmd.Parameters.AddWithValue("@gender", sv.Gender);
                cmd.Parameters.AddWithValue("@date", sv.DayOfBirth);
                cmd.Parameters.AddWithValue("@classId", sv.ClassId);
                cmd.Parameters.AddWithValue("@courseId", sv.CourseId);
                cmd.Parameters.AddWithValue("@id", sv.Id);
                #endregion

                cmd.ExecuteNonQuery();
            }          
        }
        public void AddPerResult(string id, Result rs)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                        conn.Open();

                        cmd = new SqlCommand(@"INSERT INTO HocPhan VALUES (@msv, @mmh, @qt, @tp)", conn);

                        cmd.Parameters.AddWithValue("@msv", id);
                        cmd.Parameters.AddWithValue("@mmh", rs.SubjectDetail.SubjectId);
                        cmd.Parameters.AddWithValue("@qt", rs.ScoreDetail.QT);
                        cmd.Parameters.AddWithValue("@tp", rs.ScoreDetail.TP);

                        int rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Error(ex, "Error was sent from [SQL]");
                    return;
                }

            }
            
        }
        public void Add(string id, List<Result> rl)
        {
            rl.ForEach(x =>
            {
                AddPerResult(id, x);
            });
        }
        public void UpdateScore(string id, string mmh, float dqt, float dtp)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand(@" UPDATE HOCPHAN
                                        SET DiemQT = @dqt, DiemTP = @dtp
                                        WHERE MaSV = @id AND MaMH = @mmh", conn);

                    cmd.Parameters.AddWithValue("@dqt", dqt);
                    cmd.Parameters.AddWithValue("@dtp", dtp);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@mmh", mmh);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Error(ex, "Error was sent from [SQL]");
                    return;
                }             
            }
        }
    }
}
