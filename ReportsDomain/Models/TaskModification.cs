using System;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class TaskModification
    {
        public TaskModification() {}
        public TaskModification(WorkTask workTask, TaskModificationStatus status)
        {
            WorkTask = workTask ?? throw new ReportsException("Task is null");
            Status = status;
            UpdateTime = DateTime.Now;
        }
        public WorkTask WorkTask { get; }
        public TaskModificationStatus Status { get; }
        public DateTime UpdateTime { get; }
    }
}