CREATE EXTERNAL TABLE [Audit].[AuditRulesPCDocument] (
    [Id] INT NOT NULL,
    [IdPc] INT NOT NULL,
    [Documents] VARCHAR (200) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesPCDocument'
    );

GO