using System;
using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Models
{
    public class WarehouseAudit
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }

        [Required]
        public string Status { get; set; } // Green = complete/success, Blue = active

        [Required]
        public string Location { get; set; } // e.g., Dock A, Sorting Zone 1

        [Required]
        [Display(Name = "Operator")]
        public string OperatorName { get; set; } // Used for Identity verification[cite: 1]

        public DateTime Timestamp { get; set; } = DateTime.Now; // Chronological log requirement[cite: 1]

        public string Notes { get; set; }
    }
}
