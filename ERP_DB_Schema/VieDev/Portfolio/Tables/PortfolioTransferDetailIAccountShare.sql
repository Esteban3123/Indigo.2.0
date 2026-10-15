CREATE TABLE [Portfolio].[PortfolioTransferDetailIAccountShare] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioTransferDetailId] INT             NOT NULL,
    [AccountReceivableId]       INT             NOT NULL,
    [AccountReceivableShareId]  INT             NOT NULL,
    [Value]                     NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioTransferDetailIAccountShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_PortfolioTransferDetail] FOREIGN KEY ([PortfolioTransferDetailId]) REFERENCES [Portfolio].[PortfolioTransferDetail] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioTransferDetailIAccountShare] NOCHECK CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_AccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioTransferDetailIAccountShare] NOCHECK CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_AccountReceivableShare];


GO
ALTER TABLE [Portfolio].[PortfolioTransferDetailIAccountShare] NOCHECK CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_PortfolioTransferDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por cobrar compartida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';

