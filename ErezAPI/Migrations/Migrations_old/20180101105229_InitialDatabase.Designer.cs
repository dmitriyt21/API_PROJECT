﻿// <auto-generated />
using ErezAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ErezAPI.Migrations
{
    [DbContext(typeof(AgentsActionsContext))]
    [Migration("20180101105229_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ErezAPI.EnrollActivity", b =>
                {
                    b.Property<int>("EnrollActionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgentId");

                    b.Property<string>("AgentName")
                        .HasMaxLength(50);

                    b.Property<int>("AgentPlace");

                    b.Property<double>("PercOfTarget");

                    b.Property<int>("Points");

                    b.Property<decimal>("SumOfSales");

                    b.Property<decimal>("Target");

                    b.HasKey("EnrollActionId");

                    b.ToTable("EnrollActivities");
                });

            modelBuilder.Entity("ErezAPI.OverallActivity", b =>
                {
                    b.Property<int>("EnrollActionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgentId");

                    b.Property<string>("AgentName")
                        .HasMaxLength(50);

                    b.Property<int>("AgentPlace");

                    b.Property<decimal>("EnrollPoints");

                    b.Property<int>("OverallPoints");

                    b.Property<decimal>("RetentionPoints");

                    b.HasKey("EnrollActionId");

                    b.ToTable("OverallActivities");
                });

            modelBuilder.Entity("ErezAPI.RetentionActivity", b =>
                {
                    b.Property<int>("RetentionActionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgentId");

                    b.Property<string>("AgentName")
                        .HasMaxLength(50);

                    b.Property<int>("AgentPlace");

                    b.Property<int>("NumOfExistCust");

                    b.Property<int>("NumOfNewCust");

                    b.Property<double>("PercOfTarget");

                    b.Property<int>("Points");

                    b.Property<decimal>("Target");

                    b.HasKey("RetentionActionId");

                    b.ToTable("RetentionActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
