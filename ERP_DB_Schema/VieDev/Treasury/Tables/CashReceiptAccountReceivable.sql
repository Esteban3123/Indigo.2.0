CREATE TABLE [Treasury].[CashReceiptAccountReceivable] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [CashReceiptDetailId]   INT             NOT NULL,
    [AccountReceivableId]   INT             NOT NULL,
    [InvoiceNumber]         VARCHAR (20)    NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [ValueInCurrencyHeader] NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_CashReceiptAccountReceivable__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAccountReceivable_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_CashReceiptAccountReceivable_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptAccountReceivable] NOCHECK CONSTRAINT [FK_CashReceiptAccountReceivable_AccountReceivable];


GO
ALTER TABLE [Treasury].[CashReceiptAccountReceivable] NOCHECK CONSTRAINT [FK_CashReceiptAccountReceivable_CashReceiptDetails];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del encabezado de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAccountReceivable', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';

