using System;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class TaskModification
    {
        public TaskModification() {}
        public TaskModification(Task task, TaskModificationStatus status)
        {
            Task = task ?? throw new ReportsException("Task is null");
            Status = status;
            TimeOfChange = DateTime.Now;
        }
        public Task Task { get; }
        public TaskModificationStatus Status { get; }
        public DateTime TimeOfChange { get; }
    }
}