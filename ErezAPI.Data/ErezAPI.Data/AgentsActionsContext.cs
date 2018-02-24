using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
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

        public DbSet<AlufActivity> AlufActivity { get; set; }
        public DbSet<EnrollmentActivity> EnrollmentActivity { get; set; }


        public DbSet<AlufDiff> AlufDiff { get; set; }
        public DbSet<EnrollmentDiff> EnrollmentDiff { get; set; }
        

        public DbSet<PrivateMessage> PrivateMessages { get; set; }

        public DbSet<MessageUsers> MessageUsers { get; set; }

        public DbSet<Manager> Manager { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:AgentsActionsContextConnection"]);
        }

        public class MyModelConfiguration : EntityTypeConfiguration<Manager>
        {
            public MyModelConfiguration()
            {
                ToTable("Manager");
                HasKey(x => new { x.BestAgentName, x.SalesDate });
            }
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
