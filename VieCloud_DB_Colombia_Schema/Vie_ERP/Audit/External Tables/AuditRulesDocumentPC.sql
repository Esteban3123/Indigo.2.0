CREATE EXTERNAL TABLE [Audit].[AuditRulesDocumentPC] (
    [Id] INT NOT NULL,
    [IdDocument] INT NOT NULL,
    [Pc] VARCHAR (200) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesDocumentPC'
    );

GO