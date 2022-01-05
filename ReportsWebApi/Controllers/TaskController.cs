using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportsDomain.Models;
using ReportsServices.Services;
using Task = System.Threading.Tasks.Task;
using TaskStatus = ReportsDomain.Enums.TaskStatus;

namespace ReportsWebApi.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("get-all")]
        public async Task<List<ReportsDomain.Models.Task>> GetTasks()
        {
            return await _taskService.GetAllTasks();
        }

        [HttpGet("get-id/{id}")]
        public async Task<ActionResult<ReportsDomain.Models.Task>> GetTask(Guid id)
        {
            ReportsDomain.Models.Task task = await _taskService.GetTaskById(id);

            return task;
        }

        [HttpGet("get-by-date/{date}")]
        public async Task<ActionResult<ReportsDomain.Models.Task>> GetTask([FromBody] DateTime dateTime)
        {
            ReportsDomain.Models.Task task = await _taskService.GetTaskByDate(dateTime);
            
            return task;
        }

        [HttpGet("get-by-employee/{employeeDto.Id}")]
        public async Task<ActionResult<ReportsDomain.Models.Task>> GetTask([FromBody] Employee employeeDto)
        {
            var employee = new Employee(employeeDto.Name, employeeDto.Status);
            ReportsDomain.Models.Task task = await _taskService.GetTaskByEmployee(employee);
            
            return task;
        }
        
        [HttpGet("get-all-unchanged-tasks")]
        public List<ReportsDomain.Models.Task> GetUnchangedTasks()
        {
            return _taskService.GetUnchangedTasks();
        }
        
        [HttpDelete("delete-task")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _taskService.Delete(id);
            return Content("Task creates");
        }
        
        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] TaskDTO taskDto)
        {
            var newTask = new ReportsDomain.Models.Task(taskDto.Title, taskDto.Employee, taskDto.Comment);

            await _taskService.Create(newTask);
            return Content("Task creates");
        }
        
        [HttpPatch("set-comment/{id}")]
        public async Task SetComment(Guid id, [FromBody]string comment)
        {
            await _taskService.SetTaskComment(id, comment);
        }
        
        [HttpPatch("update-task-status/{id}")]
        public async Task UpdateTaskStatus(Guid id, [FromBody]TaskStatus status)
        {
            await _taskService.UpdateTaskStatus(id, status);
        }
    }
}