CREATE TABLE [Treasury].[NoteConcepts] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [Description]           VARCHAR (255) NULL,
    [AffectBudget]          BIT           NOT NULL,
    [AutoCollect]           BIT           NOT NULL,
    [Nature]                TINYINT       NOT NULL,
    [IdMainAccount]         INT           NOT NULL,
    [Status]                BIT           CONSTRAINT [DF_NoteConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [AffectCashFlowConcept] BIT           NULL,
    [IdCashFlowConcept]     INT           NULL,
    CONSTRAINT [PK_NoteConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoteConcept_MainAccount] FOREIGN KEY ([IdMainAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_NoteConcepts_IdCashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id])
);


GO
ALTER TABLE [Treasury].[NoteConcepts] NOCHECK CONSTRAINT [FK_NoteConcept_MainAccount];


GO
ALTER TABLE [Treasury].[NoteConcepts] NOCHECK CONSTRAINT [FK_NoteConcepts_IdCashFlowConcept];

