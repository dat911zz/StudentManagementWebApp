using Castle.Windsor;
using StudentManagementWebApp.Data.Database;
using StudentManagementWebApp.Container;
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
            GetInfoSubject(kq);
            GetInfoScore(kq);         
        }
        public void GetInfoSubject(Result kq)
        {
            SubjectService mhs = container.Resolve<SubjectService>();
            mhs.GetInfo(kq.SubjectDetail);
        }
        public void GetInfoScore(Result kq)
        {
            ScoreService dsv = container.Resolve<ScoreService>();
            dsv.GetInfo(kq.ScoreDetail);
        }
    }
}
