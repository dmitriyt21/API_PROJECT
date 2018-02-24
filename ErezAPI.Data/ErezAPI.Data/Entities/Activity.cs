using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErezAPI
{
    public class Activity
    {
        public Activity()
        {

        }
        public Activity(int agentId, DateTime? salesDate, double? percOfTarget, string agentName, long? place, double? points)
        {
            AgentId = agentId;
            SalesDate = salesDate;
            PercOfTarget = percOfTarget;
            AgentName = agentName;
            Place = place;
            Points = points;
        }
        public int AgentId { get; set; }
        public DateTime? SalesDate { get; set; }
        public double? PercOfTarget { get; set; }
        public string AgentName { get; set; }
        public long? Place { get; set; }
        public double? Points { get; set; }
    }
}
