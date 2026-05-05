using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiTrack.Data;
using LogiTrack.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LogiTrack.Controllers
{
    /// <summary>
    /// Functional Requirement: Centralized Inventory & Warehouse Audit Trail
    /// Student: Adrian Chetty (ST10442488)
    /// </summary>
    public class AuditController : Controller
    {
        private readonly AppDbContext _context;

        public AuditController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Audit
        // Implements Screen 6: Searchable filters above chronological log
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var logs = from l in _context.WarehouseAuditLogs
                       select l;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Functional requirement: Searchable tracking numbers
                logs = logs.Where(s => s.TrackingNumber.Contains(searchString));
            }

            // Enterprise Logic: Chronological order for audit integrity
            return View(await logs.OrderByDescending(l => l.Timestamp).ToListAsync());
        }

        // GET: Audit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var auditLog = await _context.WarehouseAuditLogs
                .FirstOrDefaultAsync(m => m.LogId == id);

            if (auditLog == null) return NotFound();

            return View(auditLog);
        }

        // GET: Audit/LogMovement
        // Form for the Primary Workflow: Dispatch -> Assign -> Track -> Deliver
        public IActionResult LogMovement()
        {
            return View();
        }

        // POST: Audit/LogMovement
        // Implements the "Chain of Custody" and theft prevention logging
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogMovement([Bind("TrackingNumber,Status,Location,OperatorName,Notes")] WarehouseAudit warehouseAudit)
        {
            if (ModelState.IsValid)
            {
                // Ensure timestamp is set at the moment of entry for audit accuracy
                warehouseAudit.Timestamp = System.DateTime.Now;

                _context.Add(warehouseAudit);
                await _context.SaveChangesAsync();
                
                // Return to the trail to see the new entry
                return RedirectToAction(nameof(Index));
            }
            return View(warehouseAudit);
        }

        // GET: Audit/Delete/5
        // Note: In strict Audit Trails, deletions are often restricted to Admins[cite: 1]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var auditLog = await _context.WarehouseAuditLogs
                .FirstOrDefaultAsync(m => m.LogId == id);

            if (auditLog == null) return NotFound();

            return View(auditLog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auditLog = await _context.WarehouseAuditLogs.FindAsync(id);
            _context.WarehouseAuditLogs.Remove(auditLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
