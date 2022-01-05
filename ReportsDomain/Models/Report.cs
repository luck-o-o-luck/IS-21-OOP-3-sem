using System;
using System.Collections.Generic;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class Report
    {
        private List<Task> _tasks;

        public Report() {}
        public Report(Employee employee)
        {
            Writer = employee ?? throw new ReportsException("Employee is null");
            Id = Guid.NewGuid();
            Status = ReportStatus.Open;
        }
        
        public Guid Id { get; }
        public Employee Writer { get; private set; }
        public ReportStatus Status { get; private set; }
        public IEnumerable<object> Tasks => _tasks;

        public void AddTask(ReportsDomain.Models.Task task)
        {
            if (task is null)
                throw new ReportsException("Task is null");
            
            _tasks.Add(task);
        }
    }
}