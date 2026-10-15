CREATE EXTERNAL TABLE [Security].[Roll] (
    [Id] INT NOT NULL,
    [RollCode] CHAR (10) NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp] ROWVERSION NOT NULL,
    [RollType] TINYINT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Roll'
    );

