using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Contract;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private employeesContext _context;
        public EmployeeRepository(employeesContext context)
        {
            _context = context;
        }

        public Object GetEmployees(int? currentPage, int records)
        {
            //var records = 20;
            if (records > 100)
            {
                records = 100;
            }
            var page = currentPage.GetValueOrDefault(1) == 0 ? 1 : currentPage.GetValueOrDefault(1);
            var query = _context.Employees;
            var total = query.Count();
            return new
            {
                data = query.Skip((page - 1) * records).Take(records),
                total,
                page,
                last_page = total / records
            };
        }

        public Employee GetEmployee(string id)
        {
            return _context.Employees.Find(Convert.ToInt32(id));
        }

        public int UpdateEmployee(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpNo == id);
        }

        public int InsertEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            return _context.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}
