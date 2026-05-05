-- Create the WarehouseAuditLogs table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'WarehouseAuditLogs')
BEGIN
    CREATE TABLE WarehouseAuditLogs (
        LogId INT PRIMARY KEY IDENTITY(1,1),
        TrackingNumber NVARCHAR(50) NOT NULL,
        Status NVARCHAR(50) NOT NULL, -- e.g., Inbound, Sorting, Dispatched
        Location NVARCHAR(100) NOT NULL, -- e.g., Dock A, Sorting Zone 3
        OperatorName NVARCHAR(100) NOT NULL, -- Who performed the action
        Timestamp DATETIME DEFAULT GETDATE(),
        Notes NVARCHAR(MAX)
    );
END
GO
