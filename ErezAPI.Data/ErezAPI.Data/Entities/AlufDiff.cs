using System;
using System.Collections.Generic;

namespace ErezAPI
{
    public partial class AlufDiff
    {
        public int AlufDiffId { get; set; }
        public int AgentId { get; set; }
        public DateTime? SalesDate { get; set; }
        public string AgentName { get; set; }
        public long? PlaceDiff { get; set; }
        public double? AlufPointsDiff { get; set; }
        public double? Month1Diff { get; set; }
        public int? Month2Diff { get; set; }
        public int? Month3Diff { get; set; }
    }
}
