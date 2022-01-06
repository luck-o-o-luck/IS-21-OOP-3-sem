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
        Task Create(WorkTask workTask);
        Task Delete(Guid id);
        Task<List<ReportsDomain.Models.WorkTask>> GetAllTasks();
        Task<ReportsDomain.Models.WorkTask> GetTaskById(Guid id);
        Task<ReportsDomain.Models.WorkTask> GetTaskByDate(DateTime dateTime);
        Task<ReportsDomain.Models.WorkTask> GetTaskByEmployee(Employee employee);
        List<ReportsDomain.Models.WorkTask> GetUnchangedTasks();
        Task<ReportsDomain.Models.WorkTask> UpdateTaskStatus(Guid id, TaskStatus status);
        Task<ReportsDomain.Models.WorkTask> SetTaskComment(Guid id, string comment);
        public IReadOnlyList<TaskModification> GetModificationsOfTask(Guid id);
        bool Exists(Guid id);
    }
}