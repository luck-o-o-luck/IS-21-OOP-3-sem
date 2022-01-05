using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportsDomain.Enums;
using ReportsDomain.Models;
using ReportsServices.Services;
using Task = System.Threading.Tasks.Task;

namespace ReportsWebApi.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        [HttpPost]
        public async Task<Employee> Create([FromBody] EmployeeDTO employeeDto)
        {
            return  await _employeeService.Create(new Employee(employeeDto.Name, employeeDto.Status));;
        }
        
        [HttpGet("get-all")]
        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeService.GetAllEmployees();
        }
        
        [HttpPut("{employeeId}/chief")]
        public async Task<Employee> AddChief([FromRoute] Guid employeeId, [FromQuery] Guid chiefId)
        {
            return await _employeeService.AddChief(await _employeeService.FindById(employeeId), await _employeeService.FindById(chiefId));
        }
        
        [HttpGet]
        [Route("get-by-name")]
        public async Task<ActionResult<Employee>> GetEmployee
        (
            [FromQuery] string name,
            [FromQuery] string surname,
            [FromQuery] string positionName
        )
        {
            Employee employee = await _employeeService.FindByName(new NameEmployee(name, surname, positionName));
            if (employee == null) return NotFound();
            return employee;
        }
        
        [HttpGet]
        [Route("get-by-id")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(Guid id)
        {
            Employee employee = await _employeeService.FindById(id);
            if (employee == null) return NotFound();
            return new EmployeeDTO()
            {
                Name = employee.Name,
                Chief = employee.Chief,
                Status = employee.Status
            };
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.Delete(id);
            return Ok();
        }
        
        [HttpPost("add-task/{id}")]
        public async Task AddTaskToEmployee([FromBody] TaskDTO taskDto)
        {
            var newTask = new ReportsDomain.Models.Task(taskDto.Title, taskDto.Employee, taskDto.Comment);
            await _employeeService.AddTask(newTask.Employee, newTask);
        }
    }
}