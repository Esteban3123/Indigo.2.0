CREATE EXTERNAL TABLE [Security].[Email] (
    [Id] INT NOT NULL,
    [IdPerson] INT NOT NULL,
    [Email] VARCHAR (60) NOT NULL,
    [State] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Email'
    );

GO