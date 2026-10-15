CREATE TABLE [Treasury].[CashReceiptAdvancePayment] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptDetailId]   INT             NOT NULL,
    [AdvancePaymentId]      INT             NOT NULL,
    [AdvancePaymentCode]    VARCHAR (20)    NOT NULL,
    [PaymentValue]          DECIMAL (18, 2) NOT NULL,
    [ValueInCurrencyHeader] NUMERIC (20, 5) CONSTRAINT [DF__CashRecei__Value__5F1B3F67] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CashReceiptAdvancePayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAdvancePayment_AdvancePayments] FOREIGN KEY ([AdvancePaymentId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_CashReceiptAdvancePayment_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptAdvancePayment] NOCHECK CONSTRAINT [FK_CashReceiptAdvancePayment_AdvancePayments];


GO
ALTER TABLE [Treasury].[CashReceiptAdvancePayment] NOCHECK CONSTRAINT [FK_CashReceiptAdvancePayment_CashReceiptDetails];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor del reintegro expresado en la moneda de la cabecera del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se va a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anticipo que se va a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del recibo de caja que efectua el reintegro del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reintegro del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'Id';

