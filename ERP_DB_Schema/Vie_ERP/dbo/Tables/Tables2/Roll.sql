CREATE TABLE [dbo].[Roll] (
    [Id]          INT          NOT NULL,
    [RollCode]    CHAR (3)     NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp]   ROWVERSION   NOT NULL,
    [RollType]    TINYINT      NULL
);

