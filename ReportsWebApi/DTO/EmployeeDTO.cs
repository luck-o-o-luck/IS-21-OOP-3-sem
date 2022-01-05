using System;
using System.Collections.Generic;
using ReportsDomain.Enums;
using ReportsDomain.Models;

namespace ReportsWebApi
{
    public class EmployeeDTO
    {
        public NameEmployee Name { get; set; }
        public EmployeeStatus Status { get; set; }
        public Employee Chief { get; set; }
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
        public List<ReportsDomain.Models.Task> Tasks { get; set; } = new List<ReportsDomain.Models.Task>();
    }
}