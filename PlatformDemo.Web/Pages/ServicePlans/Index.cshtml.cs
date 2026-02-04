using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data;
using PlatformDemo.Web.Models;

namespace PlatformDemo.Web.Pages.ServicePlans
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Property type matches the objects we create
        public List<ServicePlanVM> ServicePlans { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            ServicePlans = _context.ServicePlans
                .Include(sp => sp.Timesheets)
                .AsEnumerable() // switch to client evaluation for TotalHours
                .Select(sp => new ServicePlanVM
                {
                    ServicePlanId = sp.ServicePlanId,
                    DateOfPurchase = sp.DateOfPurchase,
                    TimesheetCount = sp.Timesheets.Count,
                    TotalHours = sp.Timesheets.Sum(t => (t.EndTime - t.StartTime).TotalHours)
                })
                .ToList();
        }
    }

    // ViewModel used in the page
    public class ServicePlanVM
    {
        public int ServicePlanId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int TimesheetCount { get; set; }
        public double TotalHours { get; internal set; }
    }
}