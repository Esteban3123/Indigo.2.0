CREATE EXTERNAL TABLE [Security].[Address] (
    [Id] INT NOT NULL,
    [IdPerson] INT NOT NULL,
    [Addresss] VARCHAR (100) NOT NULL,
    [State] BIT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Address'
    );

GO