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
        private List<Task> _tasks;
        
        public Employee() {}
        public Employee(NameEmployee name, EmployeeStatus status)
        {
            _subordinates = new List<Employee>();
            _tasks = new List<Task>();
            Name = name ?? throw new ReportsException("Name is null");
            Status = status;
            Id = Guid.NewGuid();
        }
        
        public NameEmployee Name { get; set; }
        public Guid Id { get; set; } 
        public EmployeeStatus Status { get; set; }
        public Employee Chief { get; set; }
        public IEnumerable<object> Tasks { get; set; }

        public bool ExistsTask(Task task) => _tasks.Any(x => x.Id == task.Id);
        public bool ExistsSubordinate(Employee employee) => _subordinates.Any(x => x.Id == employee.Id);

        public void SetChief(Employee employee)
        {
            if (employee.Status == Status)
                throw new ReportsException("Can't be the chief");
            if (employee.Status == EmployeeStatus.OrdinaryEmployee)
                throw new ReportsException("Can't be the chief");
            if (employee.Status == EmployeeStatus.Supervisor && Status == EmployeeStatus.TeamLead)
                throw new ReportsException("Can't be the chief");

            Chief = employee;
        }

        public void AddTask(Task task)
        {
            if (ExistsTask(task))
                throw new ReportsException("Task already exists");
            
            _tasks.Add(task);
        }

        public void AddSubordinates(Employee employee)
        {
            if (ExistsSubordinate(employee))
                throw new ReportsException("Subordinate already exist");
            if (employee.Status == EmployeeStatus.TeamLead)
                throw new ReportsException("Can't be the subordinate");
            if (employee.Status == EmployeeStatus.Supervisor && Status != EmployeeStatus.TeamLead)
                throw new ReportsException("Can't be the subordinate");
            
            _subordinates.Add(employee);
        }
    }
}