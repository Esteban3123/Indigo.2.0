CREATE TABLE [Glosas].[GlosasPrivateParametersInterface] (
    [Id]                    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParametersInterfaceId] INT         NULL,
    [YearAccount]           INT         NULL,
    [ConceptPortfolio]      VARCHAR (3) NULL,
    [ConceptDebitNote]      VARCHAR (3) NULL,
    [ConceptCashReceipts]   VARCHAR (3) NULL,
    [ConceptCreditNotes]    VARCHAR (3) NULL,
    [TimeStamp]             ROWVERSION  NOT NULL,
    CONSTRAINT [PK_GlosasPrivateParametersInterface] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GlosasPrivateParametersInterface_GlosasParametersInterface] FOREIGN KEY ([ParametersInterfaceId]) REFERENCES [Glosas].[GlosasParametersInterface] ([Id])
);


GO
ALTER TABLE [Glosas].[GlosasPrivateParametersInterface] NOCHECK CONSTRAINT [FK_GlosasPrivateParametersInterface_GlosasParametersInterface];

