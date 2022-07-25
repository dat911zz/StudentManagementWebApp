using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models
{
    public class Role
    {
        #region Props and Attrs
        private string roleId;
        private string permId;
        public string RoleId { get => roleId; set => roleId = value; }
        public string PermId { get => permId; set => permId = value; }
        #endregion
        #region Constructors
        public Role()
        {
            RoleId = "USER";
            PermId = "READ";
        }
        public Role(string role, string perm)
        {
            RoleId = role;
            PermId = perm;
        }
        public Role(Role x)
        {
            this.roleId = x.roleId;
            this.permId = x.permId;
        }
        #endregion
    }
}