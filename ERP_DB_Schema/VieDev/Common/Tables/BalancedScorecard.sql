CREATE TABLE [Common].[BalancedScorecard] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Value]            VARCHAR (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_BalancedScorecard__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_BalancedScorecard__Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

