CREATE TABLE [Portfolio].[AccountReceivableIVACollected] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId]            INT             NOT NULL,
    [CashReceiptAccountReceivableId] INT             NULL,
    [PortfolioTransferDetailId]      INT             NULL,
    [Value]                          DECIMAL (18, 2) NOT NULL,
    [Status]                         BIT             NOT NULL,
    CONSTRAINT [PK_AccountReceivableIVACollected] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableIVACollected_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableIVACollected_CashReceiptAccountReceivable] FOREIGN KEY ([CashReceiptAccountReceivableId]) REFERENCES [Treasury].[CashReceiptAccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableIVACollected_PortfolioTransferDetail] FOREIGN KEY ([PortfolioTransferDetailId]) REFERENCES [Portfolio].[PortfolioTransferDetail] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableIVACollected] NOCHECK CONSTRAINT [FK_AccountReceivableIVACollected_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivableIVACollected] NOCHECK CONSTRAINT [FK_AccountReceivableIVACollected_CashReceiptAccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivableIVACollected] NOCHECK CONSTRAINT [FK_AccountReceivableIVACollected_PortfolioTransferDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del pago:  0 - Reversado  1 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableIVACollected', @level2type = N'COLUMN', @level2name = N'Status';

