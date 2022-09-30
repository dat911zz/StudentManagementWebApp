using StudentManagementWebApp.Interface.IData;
using StudentManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Interface.IServices
{
    public interface IResultService
    {
        List<Result> GetResultList (string id);

        void Add(string id, List<Result> rl);

        void UpdateScore(string id, string mmh, float dqt, float dtp);
    }
}
