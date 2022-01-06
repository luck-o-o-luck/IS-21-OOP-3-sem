using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsDomain.Models;
using Task = System.Threading.Tasks.Task;

namespace ReportsServices.Services
{
    public interface IReportService
    {
        Task Create(Report report);
        Task<List<WorkTask>> GetTasksForAWeek();
        Task AddNewTaskInReport(Guid reportId, WorkTask workTask);
        Task Delete(Guid reportId);
        Task<Report> FindReport(Guid reportId);
        Task<IEnumerable<Report>> GetAllReports();
    }
}