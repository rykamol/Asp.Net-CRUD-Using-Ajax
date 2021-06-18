using EmployeeManagement.Context;
using EmployeeManagement.Models.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Employee = EmployeeManagement.Models.Employee;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly EmployeeDbContext _db;

        public EmployeeController()
        {
            _db = new EmployeeDbContext();
        }

        //public async Task<ActionResult> Index()
        //{
        //    var employee = await _db.Employees.ToListAsync();
        //    return View(employee);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Index(string searchData, string Filter_Value, int? pageNo)
        //{
        //    ICollection<Employee> employee;

        //    if (searchData != null)
        //    {
        //        pageNo = 1;
        //    }

        //    else
        //    {
        //        searchData = Filter_Value;
        //    }

        //    ViewBag.FilterValue = searchData;

        //    if (!string.IsNullOrWhiteSpace(searchData))
        //    {
        //        employee = await _db.Employees
        //           .Where(emp => emp.FirstName.ToUpper().Contains(searchData.ToUpper())
        //                         || emp.LastName.ToUpper().Contains(searchData.ToUpper())
        //                         || emp.Designation.ToUpper().Contains(searchData.ToUpper())
        //                         || emp.Department.ToUpper().Contains(searchData.ToUpper())
        //                         || emp.Gender.ToUpper().Contains(searchData.ToUpper())).ToListAsync();
        //    }
        //    else
        //    {
        //        employee = await _db.Employees.ToListAsync();

        //    }
        //    int Size_Of_Page = 5;
        //    int No_Of_Page = pageNo ?? 1;
        //    return View(employee.ToPagedList(No_Of_Page, Size_Of_Page));
        //}


        public ActionResult Index()
        {
            var employees = _db.Employees.ToList();
            var viewModel = new EmployeeIndexViewModel
            {
                Employees = employees
            };
            return View(viewModel);
        }

        public JsonResult AllEmployee()
        {
            var employees = _db.Employees.ToList();
            return Json(new { Employees = employees }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(EmployeeFormViewModel viewModel)
        {
            string status = "";

            if (ModelState.IsValid)
            {
                try
                {
                    var model = new Employee()
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        JoinDate = viewModel.JoinDate,
                        CurrentSalary = viewModel.CurrentSalary,
                        Designation = viewModel.Designation,
                        Department = viewModel.Department,
                        NextReviewDate = viewModel.NextReviewDate,
                        DateOfBirth = viewModel.DateOfBirth,
                        Gender = viewModel.Gender
                    };

                    _db.Employees.Add(model);
                    await _db.SaveChangesAsync();
                    status = "Success";
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                status = "Invalid Input";
            }
            return Json(new { status = status });
        }


        //[HttpGet]
        //public async Task<ActionResult> Edit(int id)
        //{
        //    var employee = await _db.Employees.FindAsync(id);
        //    return View(employee);
        //}

        [HttpGet]
        public JsonResult EditEmployee(int id)
        {
            string status = "";
            EmployeeIndexViewModel employee = null;
            Employee dbEmployee = _db.Employees.Find(id);
            if (dbEmployee != null)
            {
                employee = new EmployeeIndexViewModel
                {
                    Id = dbEmployee.Id,
                    FirstName = dbEmployee.FirstName,
                    LastName = dbEmployee.LastName,
                    Designation = dbEmployee.Designation,
                    JoinDate = dbEmployee.JoinDate,
                    CurrentSalary = dbEmployee.CurrentSalary,
                    Department = dbEmployee.Department,
                    NextReviewDate = dbEmployee.NextReviewDate,
                    DateOfBirth = dbEmployee.DateOfBirth,
                    Gender = dbEmployee.Gender
                };
            }
            else
            {
                status = "No Employee Found!";
            }

            return Json(new { employee = employee, status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeFormViewModel model)
        {
            string status = "";

            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _db.Employees.FindAsync(model.Id);
                    if (employee != null)
                    {
                        employee.FirstName = model.FirstName;
                        employee.LastName = model.LastName;
                        employee.JoinDate = model.JoinDate;
                        employee.CurrentSalary = model.CurrentSalary;
                        employee.Designation = model.Designation;
                        employee.Department = model.Department;
                        employee.NextReviewDate = model.NextReviewDate;
                        employee.DateOfBirth = model.DateOfBirth;
                        employee.Gender = model.Gender;

                        status = "Success";
                    }
                    else
                    {
                        status = "Something went wrong";
                    }
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                status = "Invalid Input";
            }


            return Json(new
            {
                status = status
            });
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            string status = "";
            var employee = await _db.Employees.FindAsync(id);

            if (employee != null)
            {
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();
                status = "Success";
            }
            else
            {
                status = "Employee Not Found";
            }

            return Json(new
            {
                status = status
            });
        }
    }
}