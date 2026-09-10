CREATE EXTERNAL TABLE [Security].[UserOperatingUnit] (
    [Id] INT NOT NULL,
    [IdUser] INT NOT NULL,
    [IdContainer] INT NOT NULL,
    [IdOperatingUnit] INT NOT NULL,
    [Status] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'UserOperatingUnit'
    );

GO