using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ErezAPI
{
    public class Repository : IRepository
    {
        private AgentsActionsContext _context;

        public Repository(AgentsActionsContext context)
        {
            _context = context;
        }


        //EnrollmentActivities
        public IEnumerable<EnrollmentActivity> GetRecentEnrollmentActivity()
        {
            var maxDates = from e in _context.EnrollmentActivity
                           group e by e.AgentId into g
                           select new { AgentId = g.Key, maxDate = g.Max(t => t.SalesDate) };

            var results = from g in maxDates
                          join e1 in _context.EnrollmentActivity
                          on new { AgentId = g.AgentId, SalesDate = g.maxDate } equals new { AgentId = e1.AgentId, SalesDate = e1.SalesDate }
                          orderby e1.Place, e1.Points descending, e1.PercOfTarget descending
                          select e1;

            maxDates = null;

            return results;
        }

        public IEnumerable<EnrollmentActivity> GetRecentEnrollmentActivityForAgent(int id)
        {
            var maxDates = from e in _context.EnrollmentActivity
                           group e by e.AgentId into g
                           select new { AgentId = g.Key, maxDate = g.Max(t => t.SalesDate) };

            var results = from g in maxDates
                          join e1 in _context.EnrollmentActivity
                          on new { AgentId = g.AgentId, SalesDate = g.maxDate } equals new { AgentId = e1.AgentId, SalesDate = e1.SalesDate }
                          where e1.AgentId==id
                          orderby e1.Place, e1.Points descending, e1.PercOfTarget descending
                          select e1;

            maxDates = null;

            return results;
        }


        public IEnumerable<EnrollmentActivity> GetEnrollmentActivity(int id, bool? hist)
        {
            if (hist == true)
            {
                return _context.EnrollmentActivity.Where(t => t.AgentId == id).OrderByDescending(e => e.SalesDate).Skip(1).ToList();

            }
            else if (hist == false)
            {
                return(GetRecentEnrollmentActivityForAgent(id));

            }

            else
            {
                return _context.EnrollmentActivity.Where(t => t.AgentId == id).OrderByDescending(e => e.SalesDate).ToList();
            }
        }

        public IEnumerable<Manager> GetManagerData(int? ActNum, int? Month, int? WeekNum)
        {
            if (ActNum != null)
            {
                if (Month != null && WeekNum != null)
                {
                    return _context.Manager.FromSql("EXECUTE dbo.sp_DataForManagement").Where(a => a.ActivityNumber == ActNum && a.Month == Month && a.WeekNum == WeekNum).OrderBy(a => a.Activity).ThenByDescending(t => t.SalesDate).ToList();
                }

                else
                {
                    return _context.Manager.FromSql("EXECUTE dbo.sp_DataForManagement").Where(a => a.ActivityNumber == ActNum).OrderBy(a => a.Activity).ThenByDescending(t => t.SalesDate).ToList();
                }
            }
            else { return _context.Manager.FromSql("EXECUTE dbo.sp_DataForManagement").OrderBy(a => a.Activity).ThenByDescending(t => t.SalesDate).ToList(); }
            
        }


        public IEnumerable<Manager> GetLastManagerData()
        {
            var maxDate = DateTime.Now.Date;

            return _context.Manager.FromSql("EXECUTE dbo.sp_DataForManagement").Where(d => d.SalesDate==maxDate).OrderBy(a => a.Activity).ToList();
        }


        public void AppendEnrollmentActivities(List<EnrollmentActivity> EAList)
        {
            _context.EnrollmentActivity.AddRange(EAList);
        }


        public async Task<bool> SaveAllAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }



    }
}