CREATE TABLE [Treasury].[CashReceiptAccountReceivableShare] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptAccountReceivableId] INT             NOT NULL,
    [AccountReceivableShareId]       INT             NOT NULL,
    [Value]                          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CashReceiptAccountReceivableShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAccountReceivableShare_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_CashReceiptAccountReceivableShare_CashReceiptAccountReceivable] FOREIGN KEY ([CashReceiptAccountReceivableId]) REFERENCES [Treasury].[CashReceiptAccountReceivable] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptAccountReceivableShare] NOCHECK CONSTRAINT [FK_CashReceiptAccountReceivableShare_AccountReceivableShare];


GO
ALTER TABLE [Treasury].[CashReceiptAccountReceivableShare] NOCHECK CONSTRAINT [FK_CashReceiptAccountReceivableShare_CashReceiptAccountReceivable];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se va afectar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuota de la cuenta por pagar que se va afectar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detallle del recibo de caja de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'CashReceiptAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';

