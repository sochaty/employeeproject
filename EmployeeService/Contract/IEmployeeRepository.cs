using System;
using EmployeeService.Models;

namespace EmployeeService.Contract
{
    public interface IEmployeeRepository
    {
        Object GetEmployees(int? currentPage, int records);
        Employee GetEmployee(string id);
        int UpdateEmployee(Employee employee);
        bool EmployeeExists(int id);
        int InsertEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}