CREATE EXTERNAL TABLE [Audit].[AuditRuledDocumentUser] (
    [Id] INT NOT NULL,
    [IdDocument] INT NOT NULL,
    [Users] VARCHAR (200) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRuledDocumentUser'
    );

