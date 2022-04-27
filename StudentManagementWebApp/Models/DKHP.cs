using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models
{
    /// <summary>
    /// Class phụ để map tới bảng 
    /// trong DB
    /// </summary>
    public class DKHP
    {
        #region Properties
        public virtual int STT { get; set; }
        public virtual string MaSV { get; set; }
        public virtual int MH1 { get; set; }
        public virtual int MH2 { get; set; }
        public virtual int MH3 { get; set; }
        public virtual int MH4 { get; set; }
        public virtual int MH5 { get; set; }
        public virtual int MH6 { get; set; }
        public virtual int MH7 { get; set; }
        public virtual int MH8 { get; set; }
        public virtual int MH9 { get; set; }
        public virtual int MH10 { get; set; }
        #endregion
        #region Method
        public virtual int[] ToArray()
        {
            return new int[] { MH1, MH2, MH3, MH4, MH5, MH6, MH7, MH8, MH9, MH10 };
        }
        #endregion
    }
}