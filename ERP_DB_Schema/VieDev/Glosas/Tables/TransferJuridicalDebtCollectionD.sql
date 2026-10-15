CREATE TABLE [Glosas].[TransferJuridicalDebtCollectionD] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferJuridicalDebtCollectionCId] INT          NOT NULL,
    [PortfolioGlosaId]                   INT          NULL,
    [AccountReceivableId]                INT          NULL,
    [LegalTransferValue]                 MONEY        CONSTRAINT [DF__TransferJ__Legal__385FCE4B] DEFAULT ((0)) NULL,
    [InvoiceNumber]                      VARCHAR (50) NOT NULL,
    [AccountReceivableDate]              DATETIME     NULL,
    [TimeStamp]                          ROWVERSION   NOT NULL,
    [ReversedUser]                       VARCHAR (20) NULL,
    [ReversedDate]                       DATETIME     NULL,
    CONSTRAINT [PK_TransferJuridicalDebtCollectionD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionD_GlosaObjectionsReceptionC] FOREIGN KEY ([PortfolioGlosaId]) REFERENCES [Glosas].[GlosaPortfolioGlosada] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionD_TransferJuridicalDebtCollectionC] FOREIGN KEY ([TransferJuridicalDebtCollectionCId]) REFERENCES [Glosas].[TransferJuridicalDebtCollectionC] ([Id])
);


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionD] NOCHECK CONSTRAINT [AccountReceivable];


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionD] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionD_GlosaObjectionsReceptionC];


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionD] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionD_TransferJuridicalDebtCollectionC];

