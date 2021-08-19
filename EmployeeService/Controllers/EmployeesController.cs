using EmployeeService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Contract;

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: api/Employees  
        [HttpGet]
        public IActionResult GetEmployee([FromQuery(Name = "currentPage")] int currentPage, [FromQuery(Name = "records")] int records = 20)
        {
            //return await _employeesContext.Employees.ToListAsync();
            return Ok(_employeeRepository.GetEmployees(currentPage, records));
        }

        // GET: api/Employees/5  
        [HttpGet("{id}")]
        public IActionResult GetEmployee(string id)
        {
            var employee = _employeeRepository.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5  
        // To protect from spamming attacks, see https://go.microsoft.com/fwlink/?linkid=2123754  
        [HttpPut("{id}")]
        public IActionResult PutEmployee(string id, Employee employee)
        {
            if (Convert.ToInt32(id) != employee.EmpNo)
            {
                return BadRequest();
            }

            try
            {
                _employeeRepository.UpdateEmployee(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_employeeRepository.EmployeeExists(employee.EmpNo))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult PostEmployee(EmployeeService.Models.Employee employee)
        {
            _employeeRepository.InsertEmployee(employee);
            return CreatedAtAction("GetEmployee", new { id = employee.EmpNo }, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(string id)
        {
            var employee = _employeeRepository.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }
            _employeeRepository.DeleteEmployee(employee);
            return NoContent();
        }
    }
}
