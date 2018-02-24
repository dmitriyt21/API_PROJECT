using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErezAPI
{
    public class AgentsActionsContext : IdentityDbContext<ActivityUser>
    {
        private IConfigurationRoot _config;

        public AgentsActionsContext(IConfigurationRoot config, DbContextOptions options) 
      : base(options)
        {
            _config = config;
        }

        //public DbSet<EnrollActivity> EnrollActivities { get; set; }
        ////public DbSet<EnrollActivityHist> EnrollActivitiesHist { get; set; }

        ////public DbSet<EnrollActivityBased> EnrollActivitiesBased { get; set; }
        //public DbSet<RetentionActivity> RetentionActivities { get; set; }
        //public DbSet<OverallActivity_old> OverallActivities { get; set; }
        public DbSet<AlufActivity> AlufActivity { get; set; }
        public DbSet<DiscountsActivity> DiscountsActivity { get; set; }
        public DbSet<EnrollmentActivity> EnrollmentActivity { get; set; }
        //public DbSet<EnrollmentActivity2> EnrollmentActivity2 { get; set; }
        public DbSet<OverAll1Activity> OverAll1Activity { get; set; }
        public DbSet<OverAll2Activity> OverAll2Activity { get; set; }
        public DbSet<SalesActivity> SalesActivity { get; set; }
        //public DbSet<SalesActivity> SalesActivity2 { get; set; }
        public DbSet<SpecialProductsActivity> SpecialProductsActivity { get; set; }


        public DbSet<AlufDiff> AlufDiff { get; set; }
        public DbSet<OverAll1Diff> OverAll1Diff { get; set; }
        public DbSet<OverAll2Diff> OverAll2Diff { get; set; }
        public DbSet<EnrollmentDiff> EnrollmentDiff { get; set; }
        public DbSet<SalesDiff> SalesDiff { get; set; }
        public DbSet<SpecialProductsDiff> SpecialProductsDiff { get; set; }
        public DbSet<DiscountsDiff> DiscountsDiff { get; set; }
        

        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GlobalMessage> GlobalMessages { get; set; }

        public DbSet<MessageUsers> MessageUsers { get; set; }
        //public DbSet<Agent> Agents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AgentsActionsContextConnection"]);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<EnrollActivity>().ToTable("EnrollActivity");
        //    modelBuilder.Entity<EnrollActivityHist>().ToTable("EnrollActivityHist");
        //    //modelBuilder.Entity<EnrollActivityBased>().ToTable("EnrollActivityBased");
        //    modelBuilder.Entity<RetentionActivity>().ToTable("RetentionActivity");
        //    modelBuilder.Entity<OverallActivity>().ToTable("OverallActivity");
        //}
    }
}
