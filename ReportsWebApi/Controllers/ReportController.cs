using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportsDomain.Models;
using ReportsServices.Services;
using Task = System.Threading.Tasks.Task;

namespace ReportsWebApi.Controllers
{
    [Route("api/Reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        
        [HttpGet("get-all")]
        public async Task<IEnumerable<Report>> GetReports()
        {
            return await _reportService.GetAllReports();
        }
        
        [HttpPost]
        public async Task PostReport([FromBody] ReportDTO reportDto)
        {
            var newReport = new Report(reportDto.Writer);
            await _reportService.Create(newReport);
        }
        
        [HttpDelete("delete-report/{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            await _reportService.Delete(id);
            return NoContent();
        }
        
        [HttpPost("add-task-to-report/{reportId}")]
        public async Task PostReportTask(Guid reportId,[FromBody] TaskDTO taskDto)
        {
            var task = new ReportsDomain.Models.Task(taskDto.Title, taskDto.Employee, taskDto.Comment);
            await _reportService.AddNewTaskInReport(reportId, task);
        }
        
        [HttpGet("get-tasks-for-a-week")]
        public async Task<List<ReportsDomain.Models.Task>> GetReportByCurrentWeek()
        {
            return await _reportService.GetTasksForAWeek();
        }
        
        [HttpGet("get-report/{id}")]
        public async Task<ActionResult<Report>> GetReport(Guid id)
        {
            return await _reportService.FindReport(id);
        }
    }
}