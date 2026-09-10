CREATE EXTERNAL TABLE [Audit].[CategoryLog] (
    [ID] INT NOT NULL,
    [IdCategory] INT NOT NULL,
    [IdLog] INT NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Audit',
    OBJECT_NAME = N'CategoryLog'
    );

