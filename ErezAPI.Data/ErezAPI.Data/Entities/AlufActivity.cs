using System;
using System.Collections.Generic;

namespace ErezAPI
{
    public partial class AlufActivity
    {
        public int AlufActivityId { get; set; }
        public double Month1 { get; set; }
        public int Month2 { get; set; }
        public int Month3 { get; set; }
        public double AlufPoints { get; set; }
        public DateTime? SalesDate { get; set; }
        public string AgentName { get; set; }
        public int AgentId { get; set; }
        public long? Place { get; set; }
    }
}
