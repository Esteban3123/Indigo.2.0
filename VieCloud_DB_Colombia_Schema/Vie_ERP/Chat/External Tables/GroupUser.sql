CREATE EXTERNAL TABLE [Chat].[GroupUser] (
    [Id] INT NOT NULL,
    [IdUser] INT NOT NULL,
    [Name] VARCHAR (100) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Chat',
    OBJECT_NAME = N'GroupUser'
    );

GO