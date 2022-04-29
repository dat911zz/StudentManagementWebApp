using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using StudentManagement.Interface.IData;
using StudentManagement.Models;
using StudentManagement.Utilites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.ORM
{
    public class NHibernate_ : ISinhVienData, IMonHocData, ICTHocPhanData
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
        public void GetDataSV(List<SinhVien> list_sv)
        {
            int count = 0;
            using (ISession session = OpenSession())
            {
                list_sv = session.Query<SinhVien>().ToList();
                foreach (var sv in list_sv)
                {
                    Console.WriteLine("{0} \t{1} \t{2}", ++count, sv.MaSV, sv.TenSV);
                }
            }

            #region Cách cũ
            //using (var session = sefact.OpenSession())
            //{
            //    list_sv = session.Query<SinhVien>().ToList();
            //    Console.WriteLine("\nFetch the complete list again\n");
            //    var lsv = session.CreateCriteria<SinhVien>().List<SinhVien>();
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
                       .AddFromAssemblyOf<SinhVien>()
                       .AddFromAssemblyOf<MonHoc>()
                       .AddFromAssemblyOf<DKHP>()
                       )
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(false, false))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();

        }
        public List<SinhVien> GetAllSV()
        {
            List<SinhVien> list_sv = new List<SinhVien>();
            int count = 0;
            using (ISession session = OpenSession())
            {
                list_sv = session.Query<SinhVien>().ToList();
                foreach (var sv in list_sv)
                {
                    Console.WriteLine("{0} \t{1} \t{2}", ++count, sv.MaSV, sv.TenSV);
                }
            }
            return list_sv;
        }
        public List<MonHoc> GetAllMH()
        {
            List<MonHoc> list_mh = new List<MonHoc>();
            int count = 0;
            using (ISession session = OpenSession())
            {
                list_mh = session.Query<MonHoc>().ToList();
                foreach (var mh in list_mh)
                {
                    Console.WriteLine("{0} \t{1} \t{2}", ++count, mh.tenMH, mh.soTiet);
                }
            }
            return list_mh;
        }
        public void GetAllCTHP(ref List<SinhVien> list_sv, List<MonHoc> list_mh)
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
                            x.CTHP.DSMH.Add(new KetQua(new MonHoc(list_mh[i]), new Diem()));
                            Console.WriteLine("HP: {0} \t{1} \t{2}", i, list_hp[i].MaSV, list_hp[i].STT);
                        }
                        
                    }
                });
                
            }
        }
        public void Add(SinhVien sv)
        {
            throw new NotImplementedException();
        }
        public void Add(MonHoc sv)
        {
            throw new NotImplementedException();
        }
        public void Add(CTHocPhan cthp)
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
        //    //    x.ConnectionString = DatabaseHelper.GenerateConnectionString("", "SinhVien", "test01", "1234");
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
        //    //        //var sinhvien1 = new SinhVien
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
        //    //        var students = session.CreateCriteria<SinhVien>().List<SinhVien>();
        //    //        int count = 0;
        //    //        foreach (var student in students)
        //    //        {
        //    //            Console.WriteLine("{0} \t{1} \t{2} \t{3}", ++count, student.MaSV, student.TenSV, student.GioiTinh);

        //    //        }
        //    //        //session.Save(sinhvien1);
        //    //        tx.CommitAsync();//Exception
        //    //    }
        //    //}
        //    //return sefact;
        //}
        #endregion
    }
}
