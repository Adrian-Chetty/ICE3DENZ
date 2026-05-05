Data-->AppDbContext

using Microsoft.EntityFrameworkCore;
using LogiTrack.Models;

namespace LogiTrack.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // This links your Audit model to the database table
        public DbSet<WarehouseAudit> WarehouseAuditLogs { get; set; }
    }
}






DB SCRIPTS--

/* 
   Functional Requirement: Centralized Inventory & Warehouse Audit Trail
   Student: Adrian Chetty (ST10442488)
   Project: PROG7311 - Enterprise Systems
*/

-- 1. Create the WarehouseAuditLogs table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WarehouseAuditLogs')
BEGIN
    CREATE TABLE WarehouseAuditLogs (
        -- Primary Key with Identity for unique log tracking
        LogId INT PRIMARY KEY IDENTITY(1,1),

        -- The Tracking Number for the parcel movement
        TrackingNumber NVARCHAR(50) NOT NULL,

        -- Status of the movement (e.g., Inbound, Sorting, Outbound)
        [Status] NVARCHAR(50) NOT NULL,

        -- Physical location in the warehouse (e.g., Dock A, Sorting Zone 3)
        [Location] NVARCHAR(100) NOT NULL,

        -- Identity of the person performing the action for accountability[cite: 1]
        OperatorName NVARCHAR(100) NOT NULL,

        -- Automatic timestamp for the chronological audit log[cite: 1]
        [Timestamp] DATETIME DEFAULT GETDATE(),

        -- Optional field for specific incident notes or descriptions[cite: 1]
        Notes NVARCHAR(MAX) NULL
    );
    
    PRINT 'Table WarehouseAuditLogs created successfully.';
END
ELSE
BEGIN
    PRINT 'Table WarehouseAuditLogs already exists.';
END
GO

-- 2. Optional: Add sample data for testing the Audit Trail screen[cite: 1]
INSERT INTO WarehouseAuditLogs (TrackingNumber, [Status], [Location], OperatorName, Notes)
VALUES 
('LOG-9842-01', 'Inbound', 'Dock A', 'J. Smith', 'Arrived from primary carrier'),
('LOG-9842-01', 'Sorting', 'Sorting Zone 3', 'M. Johnson', 'Parcel scanned into internal inventory'),
('LOG-8721-04', 'Inbound', 'Dock B', 'S. Lee', 'High priority delivery[cite: 1]');
GO

-- 3. Verify the columns
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'WarehouseAuditLogs';
