using System;
using System.Collections.Generic;

namespace ErezAPI
{
    public partial class EnrollmentDiff
    {
        public int EnrollmentDiffId { get; set; }
        public int AgentId { get; set; }
        public DateTime? SalesDate { get; set; }
        public string AgentName { get; set; }
        public long? PlaceDiff { get; set; }
        public double? PointsDiff { get; set; }
        public double? PercOfTargetDiff { get; set; }
        public int? NewCustomersDiff { get; set; }
        public int? ExistingCustomersDiff { get; set; }
    }
}
