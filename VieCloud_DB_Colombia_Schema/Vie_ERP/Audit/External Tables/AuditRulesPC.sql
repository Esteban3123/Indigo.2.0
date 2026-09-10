CREATE EXTERNAL TABLE [Audit].[AuditRulesPC] (
    [Id] INT NOT NULL,
    [PcName] VARCHAR (200) NOT NULL,
    [UserFiler] BIT NOT NULL,
    [DocumentFilter] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesPC'
    );

GO