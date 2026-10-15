CREATE TABLE [Security].[RollInt] (
    [Id]          INT          NOT NULL,
    [RollCode]    CHAR (10)    NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp]   ROWVERSION   NOT NULL,
    [RollType]    TINYINT      NULL
);

