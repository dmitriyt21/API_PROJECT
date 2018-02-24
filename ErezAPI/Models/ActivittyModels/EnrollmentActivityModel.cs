using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErezAPI
{
    public partial class EnrollmentActivityModel
    {
        public int EnrollmentActivityId { get; set; }
        public int AgentId { get; set; }
        public int? NewCustomers { get; set; }
        public int? ExistingCustomers { get; set; }
        public DateTime? SalesDate { get; set; }
        public int? EnrollmentTarget { get; set; }
        public double? PercOfTarget { get; set; }
        public string AgentName { get; set; }
        public long? Place { get; set; }
        public double? Points { get; set; }
    }
}
