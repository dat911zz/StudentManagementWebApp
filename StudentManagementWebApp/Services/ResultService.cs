using Castle.Windsor;
using StudentManagementWebApp.Data.Database;
using StudentManagementWebApp.Installer;
using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Services
{
    public class ResultService
    {
        WindsorContainer container;

        public ResultService()
        {
            container = new WindsorContainer();
            container.Install(new ServicesInstaller());
            container.Dispose();
        }
        public void GetInfoAll(Result kq)
        {
            GetInfoMonHoc(kq);
            GetInfoDiem(kq);         
        }
        public void GetInfoMonHoc(Result kq)
        {
            SubjectService mhs = container.Resolve<SubjectService>();
            mhs.GetInfo(kq.SubjectDetail);
        }
        public void GetInfoDiem(Result kq)
        {
            ScoreService dsv = container.Resolve<ScoreService>();
            dsv.GetInfo(kq.ScoreDetail);
        }
    }
}
