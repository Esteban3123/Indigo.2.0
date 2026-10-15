CREATE EXTERNAL TABLE [Security].[ProductCatalog] (
    [Id] SMALLINT NOT NULL,
    [PlatformName] VARCHAR (100) NOT NULL,
    [SuiteName] VARCHAR (100) NOT NULL,
    [ProductName] VARCHAR (100) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'ProductCatalog'
    );

GO