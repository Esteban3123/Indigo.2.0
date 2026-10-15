CREATE EXTERNAL TABLE [Audit].[BasicAudit] (
    [Id] INT NOT NULL,
    [Tag] INT NOT NULL,
    [Entity] VARCHAR (100) NOT NULL,
    [RegisterId] VARCHAR (20) NULL,
    [UserCode] VARCHAR (20) NULL,
    [UserName] VARCHAR (200) NULL,
    [UserMachine] VARCHAR (50) NULL,
    [Parameters] VARCHAR (100) NULL,
    [ReportName] VARCHAR (50) NULL,
    [TransactionDate] DATETIME NOT NULL,
    [Operation] TINYINT NOT NULL,
    [Company] VARCHAR (2) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'BasicAudit'
    );

