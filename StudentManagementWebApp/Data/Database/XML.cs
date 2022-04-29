using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentManagement.Interface.IData;

namespace StudentManagement.Data.Database
{
    //Class for XML Databse (for future)
    public class XML : ISinhVienData, IMonHocData, ICTHocPhanData
    {
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

        public void GetAllCTHP(ref List<SinhVien> list_sv, List<MonHoc> list_mh)
        {
            throw new NotImplementedException();
        }

        public List<MonHoc> GetAllMH()
        {
            throw new NotImplementedException();
        }

        public List<SinhVien> GetAllSV()
        {
            throw new NotImplementedException();
        }
    }
}
