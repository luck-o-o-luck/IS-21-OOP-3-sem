using System;
using System.Collections.Generic;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class Report
    {
        private List<WorkTask> _tasks = new List<WorkTask>();
        
        public Report() {}
        
        public Report(Employee employee, List<WorkTask> tasks)
        {
            Writer = employee ?? throw new ReportsException("Employee is null");
            _tasks = tasks;
            Id = Guid.NewGuid();
            Status = ReportStatus.Open;
        }
        
        public Guid Id { get; }
        public Employee Writer { get; private set; }
        public ReportStatus Status { get; private set; }
        public IReadOnlyList<WorkTask> Tasks => _tasks;

        public void AddTask(WorkTask workTask)
        {
            if (workTask is null)
                throw new ReportsException("Task is null");
            
            _tasks.Add(workTask);
        }
    }
}