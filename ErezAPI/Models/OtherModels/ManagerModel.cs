using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{ 
        public partial class ManagerModel
        {
        private double? _PercOfTargetAll;
        private double? _MaxPercOfTarget;
        private double? _PercOfTargetAllPrev;
        private double? _MaxPercOfTargetPrev;
        public string Activity { get; set; }
        public int ActivityNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal? Sums { get; set; }
        public double? PercOfTargetAll { get => _PercOfTargetAll; set => _PercOfTargetAll = (double?)(Math.Round((decimal)value, 2)); }
         public double? MaxPercOfTarget { get => _MaxPercOfTarget; set => _MaxPercOfTarget = (double?)(Math.Round((decimal)value, 2)); }
        public string BestAgentName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [Display(Name = "SalesDate")]
        public DateTime? SalesDate { get; set; }
        public int? Month { get; set; }
        public int? WeekNum { get; set; }
        public int? WeekDay { get; set; }
        public decimal? SumsPrev { get; set; }
        public double? PercOfTargetAllPrev { get => _PercOfTargetAllPrev; set => _PercOfTargetAllPrev = (double?)(Math.Round((decimal)value, 2)); }
        public double? MaxPercOfTargetPrev { get => _MaxPercOfTargetPrev; set => _MaxPercOfTargetPrev = (double?)(Math.Round((decimal)value, 2)); }
        public string BestAgentNamePrev { get; set; }
    }
}
