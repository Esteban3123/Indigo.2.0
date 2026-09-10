CREATE EXTERNAL TABLE [Audit].[AuditRulesUser] (
    [Id] INT NOT NULL,
    [UserCode] VARCHAR (50) NOT NULL,
    [PcFilter] BIT NOT NULL,
    [DocumentFilter] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesUser'
    );

