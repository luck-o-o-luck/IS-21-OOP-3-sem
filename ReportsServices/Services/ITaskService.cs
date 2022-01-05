using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsDomain.Models;
using Task = System.Threading.Tasks.Task;
using TaskStatus = ReportsDomain.Enums.TaskStatus;

namespace ReportsServices.Services
{
    public interface ITaskService
    {
        Task Create(ReportsDomain.Models.Task task);
        Task Delete(Guid id);
        Task<List<ReportsDomain.Models.Task>> GetAllTasks();
        Task<ReportsDomain.Models.Task> GetTaskById(Guid id);
        Task<ReportsDomain.Models.Task> GetTaskByDate(DateTime dateTime);
        Task<ReportsDomain.Models.Task> GetTaskByEmployee(Employee employee);
        List<ReportsDomain.Models.Task> GetUnchangedTasks();
        Task<ReportsDomain.Models.Task> UpdateTaskStatus(Guid id, TaskStatus status);
        Task<ReportsDomain.Models.Task> SetTaskComment(Guid id, string comment);
        public IReadOnlyList<TaskModification> GetModificationsOfTask(Guid id);
        bool Exists(Guid id);
    }
}