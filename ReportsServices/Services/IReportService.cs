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
        Task<List<ReportsDomain.Models.Task>> GetTasksForAWeek();
        Task AddNewTaskInReport(Guid reportId, ReportsDomain.Models.Task task);
        Task Delete(Guid reportId);
        Task<Report> FindReport(Guid reportId);
        Task<IEnumerable<Report>> GetAllReports();
    }
}