using Microsoft.AspNetCore.Mvc;
using LogiTrack.Models;
using System.Collections.Generic;
using System.Linq;

namespace LogiTrack.Controllers
{
    public class AuditController : Controller
    {
        // GET: Audit/Index (The Audit Trail Table)
        public IActionResult Index(string searchString)
        {
            // Mock data representing the "Warehouse Audit Trail" screen
            var logs = new List<WarehouseAudit>
            {
                new WarehouseAudit { LogId = 1, TrackingNumber = "LOG-9842-01", Status = "Inbound", Location = "Dock A", OperatorName = "J. Smith", Timestamp = DateTime.Now.AddHours(-5) },
                new WarehouseAudit { LogId = 2, TrackingNumber = "LOG-9842-01", Status = "Sorting", Location = "Sorting Zone 3", OperatorName = "M. Johnson", Timestamp = DateTime.Now.AddHours(-4) },
                new WarehouseAudit { LogId = 3, TrackingNumber = "LOG-8721-04", Status = "Inbound", Location = "Dock B", OperatorName = "S. Lee", Timestamp = DateTime.Now.AddHours(-2) }
            };

            if (!string.IsNullOrEmpty(searchString))
            {
                logs = logs.Where(s => s.TrackingNumber.Contains(searchString)).ToList();
            }

            return View(logs);
        }

        // GET: Audit/LogMovement (Form to add a log)
        public IActionResult LogMovement()
        {
            return View();
        }
    }
}
