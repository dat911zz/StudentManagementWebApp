﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace StudentManagementWebApp.Models
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params object[] roles)
        {
            //if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
            //    throw new ArgumentException("roles");
            //this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));

            if (roles.Any(r => r.ToString().CompareTo("") == 0))
            {
                throw new ArgumentException("roles");
            }
            this.Roles = string.Join(",", roles);
        }
    }
}