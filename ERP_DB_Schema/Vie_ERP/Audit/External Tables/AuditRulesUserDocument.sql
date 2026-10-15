CREATE EXTERNAL TABLE [Audit].[AuditRulesUserDocument] (
    [Id] INT NOT NULL,
    [IdUser] INT NOT NULL,
    [Documents] VARCHAR (200) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesUserDocument'
    );

GO