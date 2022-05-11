using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Models;
using StudentManagementWebApp.Utilites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Data.ORM
{
    public class NHibernate_ : IStudentData, ISubjectData, ICourseData
    {
        private string connectionString;
        public NHibernate_(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Alternative ways for mapping class neither with .hbm.xml
        /// </summary>
        /// <param name="list_sv"></param>
        public void GetDataSV(List<Student> list_sv)
        {
            //int count = 0;
            using (ISession session = OpenSession())
            {
                list_sv = session.Query<Student>().ToList();
            }

            #region Cách cũ
            //using (var session = sefact.OpenSession())
            //{
            //    list_sv = session.Query<Student>().ToList();
            //    Console.WriteLine("\nFetch the complete list again\n");
            //    var lsv = session.CreateCriteria<Student>().List<Student>();
            //    int count = 0;
            //    foreach (var sv in lsv)
            //    {
            //        Console.WriteLine("{0} \t{1} \t{2}", ++count, sv.MaSV, sv.TenSV);
            //    }
            //}
            #endregion
        }
        /// <summary>
        /// Open Session for mapping into DB
        /// </summary>
        /// <returns>ISession</returns>
        public ISession OpenSession()
        {
            //string cntString = tconnectionString;
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                  .ConnectionString(connectionString).ShowSql()
                )
                .Mappings(m =>
                       m.FluentMappings
                       .AddFromAssemblyOf<Student>()
                       .AddFromAssemblyOf<Subject>()
                       .AddFromAssemblyOf<DKHP>()
                       )
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(false, false))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();

        }
        public List<Student> GetAllSV()
        {
            List<Student> list_sv = new List<Student>();
            //int count = 0;
            using (ISession session = OpenSession())
            {
                list_sv = session.Query<Student>().ToList();
            }
            return list_sv;
        }
        public List<Subject> GetAllMH()
        {
            List<Subject> list_mh = new List<Subject>();
            //int count = 0;
            using (ISession session = OpenSession())
            {
                list_mh = session.Query<Subject>().ToList();
            }
            return list_mh;
        }
        public void GetAllCTHP(ref List<Student> list_sv, List<Subject> list_mh)
        {
            int count = 0;

            using (ISession session = OpenSession())
            {
                var list_hp = session.Query<DKHP>().ToList();
                list_sv.ForEach(x =>
                {
                    var arr = list_hp[count++].ToArray();
                    for (int i = 0; i < list_mh.Count; i++)
                    {
                        if (arr[i] == 1)
                        {
                            x.CourseDetail.SubjectList.Add(new Result(new Subject(list_mh[i]), new Score()));
                        }
                        
                    }
                });
                
            }
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





        #region Cách cũ ,rất cồng kềnh và phức tạp
        /// <summary>
        /// Setup custom configuration for NHibernate
        /// </summary>
        /// <returns></returns>
        //public static NHibernate.ISessionFactory NHibernateSetup()
        //{
        //    //NHibernateProfiler.Initialize();
        //    //var cfg = new Configuration();
        //    ////cfg.Configure($"../../../Data/ORM/NHibernateFiles/hibernate.cfg.xml");
        //    //// This will get the current PROJECT directory
        //    //string projectDirectory = Directory.GetParent("./").Parent.Parent.FullName;
        //    //string NHibernatePath = @"\Data\ORM\";
        //    //string fullPath = projectDirectory + NHibernatePath + "hibernate.cfg.xml";
        //    ////System.Environment.GetFolderPath(Properties.Resources.hibernate_cfg.ToString());
        //    //cfg.Configure(fullPath);
        //    //return cfg.BuildSessionFactory();



        //    //NHibernateProfiler.Initialize();
        //    //var cfg = new Configuration();
        //    //string projectDirectory = Directory.GetParent("./").Parent.Parent.FullName;
        //    //string NHibernatePath = @"\Data\ORM\";
        //    //string fullPath = projectDirectory + NHibernatePath + "hibernate.cfg.xml";
        //    //cfg.Configure(fullPath);//Configure from file hibernate.cfg.xml
        //    ////string connString = @"Data Source=" + datasource + ";Initial Catalog="
        //    ////            + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;


        //    //cfg.DataBaseIntegration(x =>
        //    //{
        //    //    x.ConnectionString = DatabaseHelper.GenerateConnectionString("", "Student", "test01", "1234");
        //    //    x.Driver<SqlClientDriver>();
        //    //    x.Dialect<MsSql2012Dialect>();
        //    //    x.LogSqlInConsole = true;//Show các câu lệnh SQL khi thực hiện hàm
        //    //});

        //    //cfg.AddAssembly(Assembly.GetExecutingAssembly());
        //    //var sefact = cfg.BuildSessionFactory();
        //    //using (var session = sefact.OpenSession())
        //    //{
        //    //    //session.CreateQuery("");
        //    //    using (var tx = session.BeginTransaction())
        //    //    {
        //    //        DateTime date = new DateTime(2000, 7, 19);//yyyy-mm-dd

        //    //        //============Example For add sv into Table============
        //    //        //var Student1 = new Student
        //    //        //{
        //    //        //    MaSV = "111121",
        //    //        //    TenSV = "VCD",
        //    //        //    GioiTinh = "nam",
        //    //        //    NgaySinh = date,
        //    //        //    Lop = "11DHTH8",
        //    //        //    Khoa = "11"
        //    //        //};
        //    //        //==================================
        //    //        Console.WriteLine("\nFetch the complete list again\n");
        //    //        var students = session.CreateCriteria<Student>().List<Student>();
        //    //        int count = 0;
        //    //        foreach (var student in students)
        //    //        {
        //    //            Console.WriteLine("{0} \t{1} \t{2} \t{3}", ++count, student.MaSV, student.TenSV, student.GioiTinh);

        //    //        }
        //    //        //session.Save(Student1);
        //    //        tx.CommitAsync();//Exception
        //    //    }
        //    //}
        //    //return sefact;
        //}
        #endregion
    }
}
