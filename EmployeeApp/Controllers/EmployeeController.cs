using EmployeeApp.Context;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        HRDatabaseContext Db = new HRDatabaseContext();
        public IActionResult Index()
        {
            //List<Employee> employees = Db.Employees.ToList();
            var employees = (from employee in Db.Employees
                             join department in Db.Departments
                             on employee.DepartmentId equals department.DepartmentId
                             select new Employee
                             {
                                 EmployeeId = employee.EmployeeId,
                                 EmployeeNumber = employee.EmployeeNumber,
                                 Name = employee.Name,
                                 DOB = employee.DOB,
                                 HiringDate = employee.HiringDate,
                                 City = employee.City,
                                 GrossSalary = employee.GrossSalary,
                                 NetSalary  = employee.NetSalary,
                                 DepartmentId = employee.DepartmentId,
                                 DepartmentName = department.Name
                             }   ).ToList();
            return View(employees);
        }
        public IActionResult Create()
        {
            ViewBag.Department = Db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                Db.Employees.Add(employee);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = Db.Departments.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee emp = Db.Employees.Where(e => e.EmployeeId == id).FirstOrDefault();
            ViewBag.Department = Db.Departments.ToList();
            return View("Create",emp);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
               // Db.Entry<Employee>(employee).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
                Db.Employees.Update(employee);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = Db.Departments.ToList();
            return View();
        }
       
        public IActionResult Delete(int id)
        {
            Employee emp = Db.Employees.Where(e=>e.EmployeeId== id).FirstOrDefault();
            if(emp != null)
            {
                Db.Employees.Remove(emp);
                Db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
