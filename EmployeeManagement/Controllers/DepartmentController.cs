using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.Context;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            SqlConnection sqlCon = null;
            DataTable dt;
            using (sqlCon = new SqlConnection(Connection.SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_get_employee", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                var dr = sql_cmnd.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                sqlCon.Close();
            }

            var deptList = new List<Department>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Department dept = new Department();
                dept.DeptCode = dt.Rows[i]["Name"].ToString();
                dept.Name = dt.Rows[i]["DeptCode"].ToString();
                deptList.Add(dept);
            }
            return View(deptList);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department dept)
        {
            SqlConnection sqlCon = null;
            using (sqlCon = new SqlConnection(Connection.SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_insert_employee", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = dept.Name;
                sql_cmnd.Parameters.AddWithValue("@DeptCode", SqlDbType.NVarChar).Value = dept.DeptCode;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();
            }
            return RedirectToAction("Index");
        }
    }
}