CREATE EXTERNAL TABLE [Audit].[AuditDetail] (
    [Id] INT NOT NULL,
    [IdAudit] INT NOT NULL,
    [AssociatedTable] VARCHAR (50) NULL,
    [IdAuditAssociated] INT NULL,
    [Property] VARCHAR (100) NOT NULL,
    [PreviousValue] VARCHAR (MAX) NULL,
    [NewValue] VARCHAR (MAX) NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'AuditDetail'
    );

GO