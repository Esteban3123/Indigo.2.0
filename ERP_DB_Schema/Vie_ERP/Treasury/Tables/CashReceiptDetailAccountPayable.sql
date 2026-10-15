CREATE TABLE [Treasury].[CashReceiptDetailAccountPayable] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptDetailId] INT             NOT NULL,
    [AccountPayableId]    INT             NOT NULL,
    [RefundValue]         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CashReceiptDetailAccountPatable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptDetailAccountPayable_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_CashReceiptDetailAccountPayable_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptDetailAccountPayable] NOCHECK CONSTRAINT [FK_CashReceiptDetailAccountPayable_AccountPayable];


GO
ALTER TABLE [Treasury].[CashReceiptDetailAccountPayable] NOCHECK CONSTRAINT [FK_CashReceiptDetailAccountPayable_CashReceiptDetails];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del reintegro de la CxP', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'RefundValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar que se va a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del recibo de caja que efectua el reintegro de la CxP', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabl', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';

