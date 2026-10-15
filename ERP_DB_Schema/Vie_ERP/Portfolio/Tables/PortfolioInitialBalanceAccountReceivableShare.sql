CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableShare] (
    [Id]                                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceAccountReceivableId] INT          NOT NULL,
    [Number]                                     INT          NOT NULL,
    [ExpiredDate]                                DATETIME     NOT NULL,
    [Value]                                      NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivableShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableShare_PortfolioInitialBalanceAccountReceivable] FOREIGN KEY ([PortfolioInitialBalanceAccountReceivableId]) REFERENCES [Portfolio].[PortfolioInitialBalanceAccountReceivable] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableShare] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableShare_PortfolioInitialBalanceAccountReceivable];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de la cuenta por pagar del saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';

