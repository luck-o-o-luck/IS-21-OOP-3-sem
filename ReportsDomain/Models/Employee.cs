using System;
using System.Collections.Generic;
using System.Linq;
using ReportsDomain.Enums;
using ReportsDomain.Tools;

namespace ReportsDomain.Models
{
    public class Employee
    {
        private List<Employee> _subordinates;
        private List<WorkTask> _tasks;
        
        public Employee() {}
        public Employee(NameEmployee name, EmployeeStatus status)
        {
            _subordinates = new List<Employee>();
            _tasks = new List<WorkTask>();
            Name = name ?? throw new ReportsException("Name is null");
            Status = status;
            Id = Guid.NewGuid();
        }
        
        public NameEmployee Name { get; set; }
        public Guid Id { get; set; } 
        public EmployeeStatus Status { get; set; }
        public Employee Chief { get; set; }
        public IReadOnlyList<WorkTask> Tasks => _tasks;

        public bool ExistsTask(WorkTask workTask) => _tasks.Any(x => x.Id == workTask.Id);
        public bool ExistsSubordinate(Employee employee) => _subordinates.Any(x => x.Id == employee.Id);

        public void SetChief(Employee employee)
        {
            if (employee.Status <= Status)
                throw new ReportsException("Can't be the chief");

            Chief = employee;
        }

        public void AddTask(WorkTask workTask)
        {
            if (ExistsTask(workTask))
                throw new ReportsException("Task already exists");
            
            _tasks.Add(workTask);
        }

        public void AddSubordinates(Employee employee)
        {
            if (ExistsSubordinate(employee))
                throw new ReportsException("Subordinate already exist");
            if (employee.Status >= Status)
                throw new ReportsException("Can't be the subordinate");

            _subordinates.Add(employee);
        }
    }
}