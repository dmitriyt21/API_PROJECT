using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{
    public partial class EnrollmentActivity:Activity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentActivityId { get; set; }

        public EnrollmentActivity(){}

        public EnrollmentActivity(int agentId, string agentName, long? place, double? percOfTarget, double? points, DateTime? salesDate, int? enrollTarget, int? newCustomers, int? existingCustomers)
            :base(agentId, salesDate, percOfTarget, agentName, place, points)
        {
            NewCustomers = newCustomers;
            ExistingCustomers = existingCustomers;
            EnrollmentTarget = enrollTarget;

        }
        public int? NewCustomers { get; set; }
        public int? ExistingCustomers { get; set; }
        public int? EnrollmentTarget { get; set; }
    }
}
