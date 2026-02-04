using System;

namespace PlatformDemo.Web.Models
{
    public class ServicePlanVM
    {
        public int ServicePlanId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int TimesheetCount { get; set; }
        public double TotalHours { get; set; }
    }
}
