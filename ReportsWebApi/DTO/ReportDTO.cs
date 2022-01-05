using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReportsDomain.Enums;
using ReportsDomain.Models;

namespace ReportsWebApi
{
    public class ReportDTO
    {
        public Employee Writer { get; set; }
        public ReportStatus Status { get; set; }
        public List<ReportsDomain.Models.Task> Tasks { get; set; } = new List<ReportsDomain.Models.Task>();
    }
}