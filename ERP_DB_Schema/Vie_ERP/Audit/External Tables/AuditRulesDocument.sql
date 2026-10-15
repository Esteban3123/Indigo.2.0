CREATE EXTERNAL TABLE [Audit].[AuditRulesDocument] (
    [Id] INT NOT NULL,
    [DocumentCode] VARCHAR (200) NOT NULL,
    [UserFilter] BIT NOT NULL,
    [PcFilter] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditRulesDocument'
    );

GO
