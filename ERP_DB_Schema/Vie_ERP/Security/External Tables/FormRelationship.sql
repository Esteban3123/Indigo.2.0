CREATE EXTERNAL TABLE [Security].[FormRelationship] (
    [Id] INT NOT NULL,
    [IdErpForm] VARCHAR (5) NOT NULL,
    [IdHisForm] CHAR (3) NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'FormRelationship'
    );

GO