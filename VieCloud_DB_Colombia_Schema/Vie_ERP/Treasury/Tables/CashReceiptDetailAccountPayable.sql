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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) del reintegro, devolución o abono aplicado a la cuenta por pagar desde el recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'RefundValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del reintegro de la CxP', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'RefundValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'RefundValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar/obligación (FK → Payments.AccountPayable) que será reintegrada o devuelta mediante el recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar que se va a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del recibo de caja (FK → Treasury.CashReceiptDetails) que registra el reintegro o devolución de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del recibo de caja que efectua el reintegro de la CxP', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la relación entre recibo de caja y cuenta por pagar a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabl', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de los recibos de caja aplicados a cuentas por pagar, indicando el valor reembolsado o cruzado para cada cuenta por pagar asociada a un recibo de caja específico.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptDetailAccountPayable';
