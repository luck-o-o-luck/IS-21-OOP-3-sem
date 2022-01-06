using System;
using System.Collections.Generic;
using ReportsDomain.Models;

namespace ReportsWebApi
{
    public class TaskDto
    {
        public ReportsDomain.Enums.TaskStatus Status { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public Employee Employee { get; set; }
        private List<TaskModification> TaskModifications { get; set; } = new List<TaskModification>();
    }
}