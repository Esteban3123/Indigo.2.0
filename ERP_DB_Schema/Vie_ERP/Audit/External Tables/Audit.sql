CREATE EXTERNAL TABLE [Audit].[Audit] (
    [Id] INT NOT NULL,
    [Action] TINYINT NOT NULL,
    [Entity] VARCHAR (50) NOT NULL,
    [EntityKey] INT NOT NULL,
    [IdAudit] INT NULL,
    [Date] DATETIME NOT NULL,
    [Users] VARCHAR (50) NOT NULL,
    [Form] VARCHAR (50) NOT NULL,
    [UserWindows] VARCHAR (250) NOT NULL,
    [Workstation] VARCHAR (100) NOT NULL,
    [Application] VARCHAR (100) NULL,
    [Company] VARCHAR (2) NOT NULL,
    [IsParent] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'Audit'
    );

GO