CREATE EXTERNAL TABLE [Audit].[AuditRulesUserPC] (
    [Id] INT NOT NULL,
    [IdUser] INT NOT NULL,
    [PcName] VARCHAR (200) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesUserPC'
    );

