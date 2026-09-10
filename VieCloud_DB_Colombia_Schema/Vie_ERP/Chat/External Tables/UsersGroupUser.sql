CREATE EXTERNAL TABLE [Chat].[UsersGroupUser] (
    [Id] INT NOT NULL,
    [IdGroupUser] INT NOT NULL,
    [IdUser] INT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Chat',
    OBJECT_NAME = N'UsersGroupUser'
    );

GO