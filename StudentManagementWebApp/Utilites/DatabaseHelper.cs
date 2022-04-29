using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementWebApp.Utilites
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Generate a connection string of database
        /// </summary>
        /// <param name="datasource">Server name</param>
        /// <param name="db">Database name</param>
        /// <param name="username">Name of user</param>
        /// <param name="pass">Password of the user</param>
        public static string GenerateConnectionString(string datasource, string db, string username, string pass)
        {
            //
            // Data Source=<Server-Name>;Initial Catalog=<table>;Persist Security Info=True;User ID=<username>;Password=<password>
            //
            return @"Data Source=" + datasource + ";Initial Catalog="
                        + db + ";Persist Security Info=True;User ID=" + username + ";Password=" + pass;
        }
    }
}
