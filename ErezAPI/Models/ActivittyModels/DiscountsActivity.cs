using System;
using System.Collections.Generic;

namespace ErezAPI
{
    public partial class DiscountsActivity
    {
        public int DiscountsActivityId { get; set; }
        public int AgentId { get; set; }
        public double? DiscountPercent { get; set; }
        public int? SalesNumber { get; set; }
        public DateTime? SalesDate { get; set; }
        public double? CustomerDiscountTarget { get; set; }
        public double? PercOfTarget { get; set; }
        public string AgentName { get; set; }
        public long? Place { get; set; }
        public double? Points { get; set; }
    }
}
