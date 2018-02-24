using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{
    public interface IRepository
    {
        Task<bool> SaveAllAsync();

        // Enrollment
        IEnumerable<EnrollmentActivity> GetRecentEnrollmentActivity();
        IEnumerable<EnrollmentActivity> GetRecentEnrollmentActivityForAgent(int id);
        IEnumerable<EnrollmentActivity> GetEnrollmentActivity(int id, bool? hist);
        IEnumerable<Manager> GetManagerData(int? ActNum, int? Month, int? WeekNum);
        IEnumerable<Manager> GetLastManagerData();
        void AppendEnrollmentActivities(List<EnrollmentActivity> EAList);
    }
}
