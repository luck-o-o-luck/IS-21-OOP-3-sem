using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsDataAccess.DataBase;
using ReportsDomain.Models;
using ReportsDomain.Tools;
using Task = System.Threading.Tasks.Task;

namespace ReportsServices.Services
{
    public class ReportService : IReportService
    {
        private readonly ReportsDbContext _context;
        
        public ReportService(ReportsDbContext context) => _context = context;

        public bool Exist(Report report) => _context.Reports.Any(r => r.Id == report.Id);
        
        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _context.Reports.ToListAsync();
        }
        
        public async Task Create(Report report)
        {
            if (Exist(report))
                throw new ReportsException("Report already exists");
            
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkTask>> GetTasksForAWeek()
        {
            DateTime start = DateTime.Now;
            while (start.DayOfWeek != DayOfWeek.Monday)
            { 
                start.AddDays(-1);
            }
            
            var selectedTasks =
                _context.Tasks.Where(task => task.IsTaskForWeek(start)).ToList();

            return await Task.FromResult(selectedTasks);
        }

        public async Task AddNewTaskInReport(Guid reportId, WorkTask workTask)
        {
            Report report = FindReport(reportId).Result;
            report.AddTask(workTask);

            _context.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid reportId)
        {
            Report report = FindReport(reportId).Result;

            _context.Reports.Remove(report);
            _context.Update(report);
            await _context.SaveChangesAsync();
        }

        public async Task<Report> FindReport(Guid reportId)
        {
           Report selectedReport =  await _context.Reports.FindAsync(reportId);

           return selectedReport;
        }
    }
}