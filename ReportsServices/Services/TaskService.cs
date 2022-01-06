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

        public async Task Create(WorkTask workTask)
        {
            if (Exists(workTask.Id))
                throw new ReportsException("Task already exists");
            
            _context.Tasks.Add(workTask);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            WorkTask workTask = await _context.Tasks.FindAsync(id);

            if (workTask is null)
                throw new ReportsException("Task doesn't exist");
            
            _context.Tasks.Remove(workTask);
            _context.Update(workTask);
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkTask>> GetAllTasks() => await _context.Tasks.ToListAsync();

        public IReadOnlyList<TaskModification> GetModificationsOfTask(Guid id)
        {
            var selectedTask = _context.Tasks.Find(id).Modifications.ToList();

            return selectedTask;
        }

        public async Task<WorkTask> GetTaskById(Guid id)
        {
            WorkTask workTask = await _context.Tasks.FindAsync(id);

            return workTask;
        }

        public async Task<WorkTask> GetTaskByDate(DateTime dateTime)
        {
            WorkTask selectedWorkTask =
               await _context.Tasks.FirstOrDefaultAsync(task => task.CreationTime == dateTime);

            return selectedWorkTask;
        }

        public async Task<WorkTask> GetTaskByEmployee(Employee employee)
        {
            WorkTask selectedWorkTask =
                await _context.Tasks.FirstOrDefaultAsync(task => task.Employee == employee);

            return selectedWorkTask;
        }

        public List<WorkTask> GetUnchangedTasks()
        {
           var selectedTasks = 
               _context.Tasks.Where(task => task.Modifications.Count == 0).ToList();

            return selectedTasks;
        }

        public async Task<WorkTask> UpdateTaskStatus(Guid id, TaskStatus status)
        {
            WorkTask workTask = await _context.Tasks.FindAsync(id);
            
            if (workTask is null)
                throw new ReportsException("Task doesn't exist");
            
            workTask.SetStatus(status);
            _context.Update(workTask);
            await _context.SaveChangesAsync();

            return workTask;
        }

        public async Task<WorkTask> SetTaskComment(Guid id, string comment)
        {
            WorkTask workTask = await _context.Tasks.FindAsync(id);
            
            if (workTask is null)
                throw new ReportsException("Task doesn't exist");
            
            workTask.SetComment(comment);
            _context.Update(workTask);
            
            await _context.SaveChangesAsync();

            return workTask;
        }

        public bool Exists(Guid id) => _context.Tasks.Any(task => task.Id == id);
    }
}