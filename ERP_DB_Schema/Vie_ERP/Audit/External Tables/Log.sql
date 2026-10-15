CREATE EXTERNAL TABLE [Audit].[Log] (
    [Id] INT NOT NULL,
    [IdEvent] INT NULL,
    [Priority] INT NOT NULL,
    [Severity] NVARCHAR (32) NOT NULL,
    [Title] NVARCHAR (256) NOT NULL,
    [TimeSpam] DATETIME NOT NULL,
    [WorkstationName] NVARCHAR (32) NOT NULL,
    [AppDomainName] NVARCHAR (512) NOT NULL,
    [ProcessId] NVARCHAR (256) NOT NULL,
    [ProcessName] NVARCHAR (512) NOT NULL,
    [ThreadName] NVARCHAR (512) NULL,
    [Win32ThreadId] NVARCHAR (128) NULL,
    [Message] NVARCHAR (1500) NULL,
    [FormatedMessage] NVARCHAR (1500) NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'Log'
    );

