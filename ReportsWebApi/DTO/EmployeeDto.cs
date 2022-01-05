using System;
using System.Collections.Generic;
using ReportsDomain.Enums;
using ReportsDomain.Models;

namespace ReportsWebApi
{
    public class EmployeeDto
    {
        public NameEmployee Name { get; set; }
        public EmployeeStatus Status { get; set; }
        public Employee Chief { get; set; }
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
        public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    }
}