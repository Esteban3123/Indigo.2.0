CREATE EXTERNAL TABLE [Security].[PhoneType] (
    [Id] SMALLINT NOT NULL,
    [Code] VARCHAR (1) NOT NULL,
    [Name] VARCHAR (30) NOT NULL,
    [State] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'PhoneType'
    );

GO