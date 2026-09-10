CREATE EXTERNAL TABLE [Audit].[Category] (
    [Id] INT NOT NULL,
    [CategoryName] NVARCHAR (64) NOT NULL,
    [TimeSpam] BIGINT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'Category'
    );

