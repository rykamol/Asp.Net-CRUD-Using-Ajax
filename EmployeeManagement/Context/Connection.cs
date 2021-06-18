using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Context
{
    public static class Connection
    {
        public static string SqlconString = ConfigurationManager.ConnectionStrings["EmployeeDbContext"].ConnectionString;
    }
}