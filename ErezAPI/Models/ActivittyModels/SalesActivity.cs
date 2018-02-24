using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErezAPI
{
    public partial class SalesActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesActivityId { get; set; }
        public int AgentId { get; set; }
        public decimal? Sales { get; set; }
        public DateTime? SalesDate { get; set; }
        public decimal? SalesTarget { get; set; }
        public double? PercOfTarget { get; set; }
        public string AgentName { get; set; }
        public long? Place { get; set; }
        public double? Points { get; set; }
    }
}
