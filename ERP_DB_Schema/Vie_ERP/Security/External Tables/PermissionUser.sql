CREATE EXTERNAL TABLE [Security].[PermissionUser] (
    [Id] INT NOT NULL,
    [IdUser] INT NOT NULL,
    [IdForm] VARCHAR (5) NOT NULL,
    [Action] VARCHAR (3) NOT NULL,
    [ActionValue] BIT NOT NULL,
    [TenantId] SMALLINT NULL,
    [TimeStamp] ROWVERSION NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'PermissionUser'
    );

GO