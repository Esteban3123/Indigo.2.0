CREATE EXTERNAL TABLE [Audit].[AuditRulesPCUser] (
    [Id] INT NOT NULL,
    [IdPc] INT NOT NULL,
    [Users] VARCHAR (200) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesPCUser'
    );

