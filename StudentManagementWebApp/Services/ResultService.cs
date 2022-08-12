using Castle.Windsor;
using StudentManagementWebApp.Container;
using StudentManagementWebApp.Models;
using System.Collections.Generic;
using StudentManagementWebApp.Interface.IServices;
using StudentManagementWebApp.Interface.IData;

namespace StudentManagementWebApp.Services
{
    public class ResultService : IResultService
    {
        IResultData _resultData;

        public ResultService(IResultData resultData)
        {
            _resultData = resultData;
        }
        public void Add(string id, List<Result> rl)
        {
            _resultData.Add(id, rl);
        }
        public void Remove()
        {

        }
        public void UpdateScore(string id, string mmh, float dqt, float dtp)
        {
            _resultData.UpdateScore(id, mmh, dqt, dtp);
        }
        public List<Result> GetResultList(string id)
        {
            return _resultData.GetResultList(id);
        }
    }
}
