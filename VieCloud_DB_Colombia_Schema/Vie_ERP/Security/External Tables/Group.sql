CREATE EXTERNAL TABLE [Security].[Group] (
    [Id] INT NOT NULL,
    [Code] VARCHAR (3) NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp] ROWVERSION NOT NULL,
    [State] BIT NOT NULL,
    [Synchronized] VARCHAR (1) NOT NULL,
    [GroupType] TINYINT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'Group'
    );

GO