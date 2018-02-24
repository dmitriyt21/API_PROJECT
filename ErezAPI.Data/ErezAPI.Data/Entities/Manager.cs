using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{ 
        public partial class Manager
        {
        [Key]
        public System.Guid ID { get; set; }
        public string Activity { get; set; }
        public int ActivityNumber { get; set; }
        public decimal? Sums { get; set; }
        public double? PercOfTargetAll { get; set; }
        public double? MaxPercOfTarget { get; set; }
        public string BestAgentName { get; set; }
        public DateTime? SalesDate { get; set; }
        public int? Month { get; set; }
        public int? WeekNum { get; set; }
        public int? WeekDay { get; set; }
        public decimal? SumsPrev { get; set; }
        public double? PercOfTargetAllPrev { get; set; }
        public double? MaxPercOfTargetPrev { get; set; }
        public string BestAgentNamePrev { get; set; }

    }
}
