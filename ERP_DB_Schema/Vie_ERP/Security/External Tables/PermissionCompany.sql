CREATE EXTERNAL TABLE [Security].[PermissionCompany] (
    [Id] INT NOT NULL,
    [IdUser] INT NOT NULL,
    [IdContainer] INT NOT NULL,
    [IdOperatingUnitDefault] INT NOT NULL,
    [Permission] BIT NOT NULL,
    [Administrator] BIT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'PermissionCompany'
    );

GO