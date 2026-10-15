CREATE TABLE [Treasury].[CashFlowConcept] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [NameConcept]      VARCHAR (100) NOT NULL,
    [TypeConcept]      TINYINT       NOT NULL,
    [Activity]         TINYINT       NOT NULL,
    [StatusConcept]    BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CashFlowConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

