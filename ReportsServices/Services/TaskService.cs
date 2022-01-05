using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportsDataAccess.DataBase;
using ReportsDomain.Enums;
using ReportsDomain.Models;
using ReportsDomain.Tools;
using Task = System.Threading.Tasks.Task;
using TaskStatus = ReportsDomain.Enums.TaskStatus;

namespace ReportsServices.Services
{
    public class TaskService : ITaskService
    {
        private readonly ReportsDbContext _context;

        public TaskService(ReportsDbContext reportsDbContext) => _context = reportsDbContext;

        public async Task Create(ReportsDomain.Models.Task task)
        {
            if (Exists(task.Id))
                throw new ReportsException("Task already exists");
            
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            ReportsDomain.Models.Task task = await _context.Tasks.FindAsync(id);

            if (task is null)
                throw new ReportsException("Task doesn't exist");
            
            _context.Tasks.Remove(task);
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReportsDomain.Models.Task>> GetAllTasks() => await _context.Tasks.ToListAsync();

        public IReadOnlyList<TaskModification> GetModificationsOfTask(Guid id)
        {
            var selectedTask = _context.Tasks.Find(id).Modifications.ToList();

            return selectedTask;
        }

        public async Task<ReportsDomain.Models.Task> GetTaskById(Guid id)
        {
            ReportsDomain.Models.Task task = await _context.Tasks.FindAsync(id);

            return task;
        }

        public async Task<ReportsDomain.Models.Task> GetTaskByDate(DateTime dateTime)
        {
            ReportsDomain.Models.Task selectedTask =
               await _context.Tasks.FirstOrDefaultAsync(task => task.CreationTime == dateTime);

            return selectedTask;
        }

        public async Task<ReportsDomain.Models.Task> GetTaskByEmployee(Employee employee)
        {
            ReportsDomain.Models.Task selectedTask =
                await _context.Tasks.FirstOrDefaultAsync(task => task.Employee == employee);

            return selectedTask;
        }

        public List<ReportsDomain.Models.Task> GetUnchangedTasks()
        {
           var selectedTasks = 
               _context.Tasks.Where(task => task.Modifications.Count == 0).ToList();

            return selectedTasks;
        }

        public async Task<ReportsDomain.Models.Task> UpdateTaskStatus(Guid id, TaskStatus status)
        {
            ReportsDomain.Models.Task task = await _context.Tasks.FindAsync(id);
            
            if (task is null)
                throw new ReportsException("Task doesn't exist");
            
            task.SetStatus(status);
            _context.Update(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<ReportsDomain.Models.Task> SetTaskComment(Guid id, string comment)
        {
            ReportsDomain.Models.Task task = await _context.Tasks.FindAsync(id);
            
            if (task is null)
                throw new ReportsException("Task doesn't exist");
            
            task.SetComment(comment);
            _context.Update(task);
            
            await _context.SaveChangesAsync();

            return task;
        }

        public bool Exists(Guid id) => _context.Tasks.Any(task => task.Id == id);
    }
}