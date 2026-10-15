CREATE TABLE [Budget].[LevelCategory] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Length]           INT           NOT NULL,
    [Level]            INT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_LevelCategory_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_LevelCategory_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_LevelCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

