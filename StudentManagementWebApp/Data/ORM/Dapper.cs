using Dapper;
using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StudentManagementWebApp.Data.ORM
{
    /// <summary>
    /// Class for Dapper Library
    /// </summary>
    public class Dapper : IStudentData, ISubjectData
    {
        private readonly string connectionString;
        public Dapper(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Student> GetAllSV()
        {
            List<Student> list_sv = new List<Student>();
            string sql = "SELECT * FROM SinhVien";
            using (var conn = new SqlConnection(connectionString))
            {
                list_sv = conn.Query<Student>(sql).AsList();
            }
            return list_sv;
        }
        public List<Subject> GetAllMH()
        {
            List<Subject> list_mh = new List<Subject>();
            string sql = "SELECT * FROM MonHoc";
            using (var conn = new SqlConnection(connectionString))
            {
                list_mh = conn.Query<Subject>(sql).AsList();
            }
            return list_mh;
        }

        public void Add(Course cthp)
        {
            throw new System.NotImplementedException();
        }
        public void Add(Student sv)
        {
            throw new System.NotImplementedException();
        }
        public void Add(Subject sv)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Student sv)
        {
            throw new System.NotImplementedException();
        }

        public void GetAllCTHP(ref List<Student> list_sv)
        {
            throw new System.NotImplementedException();
        }
    }
    
}