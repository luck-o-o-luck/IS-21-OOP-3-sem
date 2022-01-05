using System;
using System.Collections.Generic;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class Task
    {
        private List<TaskModification> _taskModifications;

        public Task() {}
        public Task(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ReportsException("Title is null");
            
            Title = title;
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            Status = Enums.TaskStatus.Open;
            _taskModifications = new List<TaskModification>();
        }
        public Task(string title, Employee employee, string comment)
        {
            if (string.IsNullOrEmpty(title))
                throw new ReportsException("Title is null");
            
            Title = title;
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            Status = Enums.TaskStatus.Open;
            _taskModifications = new List<TaskModification>();
            Employee = employee;
            Comment = comment;
        }
        
        public Guid Id { get; }
        public Enums.TaskStatus Status { get; private set; }
        public string Title { get; }
        public string Comment { get; private set; }
        public DateTime CreationTime { get; }
        public Employee Employee { get; private set; }
        public List<TaskModification> Modifications => _taskModifications;

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