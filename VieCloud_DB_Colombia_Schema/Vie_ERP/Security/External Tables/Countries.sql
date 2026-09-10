CREATE EXTERNAL TABLE [Security].[Countries] (
    [Id] TINYINT NOT NULL,
    [Code] VARCHAR (3) NOT NULL,
    [Name] VARCHAR (200) NOT NULL,
    [Flagcode] VARCHAR (10) NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Countries'
    );

GO