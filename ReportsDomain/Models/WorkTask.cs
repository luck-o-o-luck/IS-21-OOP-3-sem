using System;
using System.Collections.Generic;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class WorkTask
    {
        private List<TaskModification> _taskModifications;

        public WorkTask() {}
        public WorkTask(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ReportsException("Title is null");
            
            Title = title;
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            Status = Enums.TaskStatus.Open;
            _taskModifications = new List<TaskModification>();
        }
        public WorkTask(string title, Employee employee, string comment)
        {
            if (string.IsNullOrEmpty(title))
                throw new ReportsException("Title is null or empty");
            
            Title = title;
            Employee = employee;
            Comment = comment;
            
            Id = Guid.NewGuid();
            _taskModifications = new List<TaskModification>();
            CreationTime = DateTime.Now;
            Status = TaskStatus.Open;
        }
        
        public Guid Id { get; }
        public Enums.TaskStatus Status { get; private set; }
        public string Title { get; }
        public string Comment { get; private set; }
        public DateTime CreationTime { get; }
        public Employee Employee { get; private set; }
        public List<TaskModification> Modifications => _taskModifications;
        public bool IsTaskForWeek(DateTime start) => CreationTime <= DateTime.Now && CreationTime >= start;

        public void SetEmployee(Employee employee)
        {
            Employee = employee ?? throw new ReportsException("Employee is null");
            
            _taskModifications.Add(new TaskModification(this, TaskModificationStatus.ChangeEmployee));
        }

        public void SetComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
                throw new ReportsException("Comment doesn't exist");

            Comment = comment;
            Status = Enums.TaskStatus.Active;
            _taskModifications.Add(new TaskModification(this, TaskModificationStatus.AddComment));
        }

        public void SetStatus(Enums.TaskStatus status)
        {
            Status = status;
            
            _taskModifications.Add(new TaskModification(this, TaskModificationStatus.ChangeStatus));
        }
    }
}