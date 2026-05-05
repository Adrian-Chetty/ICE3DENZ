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
        public string Status { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Operator")]
        public string OperatorName { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public string Notes { get; set; }
    }
}