
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace PlatformDemo.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ServicePlan> ServicePlans { get; set; }
    public DbSet<Timesheet> Timesheets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var servicePlans = new List<ServicePlan>();
        var timesheets = new List<Timesheet>();
        var rand = new Random();

        for (int i = 1; i <= 12; i++)
        {
            servicePlans.Add(new ServicePlan
            {
                ServicePlanId = i,
                DateOfPurchase = DateTime.Now.AddDays(-i * 10)
            });

            int count = rand.Next(0, 6);
            for (int j = 1; j <= count; j++)
            {
                timesheets.Add(new Timesheet
                {
                    TimesheetId = i * 10 + j,
                    ServicePlanId = i,
                    StartTime = DateTime.Now.AddHours(-j * 2),
                    EndTime = DateTime.Now.AddHours(-j),
                    Description = "Seeded work entry"
                });
            }
        }

        modelBuilder.Entity<ServicePlan>().HasData(servicePlans);
        modelBuilder.Entity<Timesheet>().HasData(timesheets);
    }
}

public class ServicePlan
{
    public int ServicePlanId { get; set; }
    public DateTime DateOfPurchase { get; set; }
    public ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
}

public class Timesheet
{
    public int TimesheetId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    public int ServicePlanId { get; set; }
}
