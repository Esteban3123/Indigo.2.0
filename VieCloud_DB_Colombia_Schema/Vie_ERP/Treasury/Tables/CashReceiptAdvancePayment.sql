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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del reintegro (devolución/reembolso) del anticipo expresado en la moneda de la cabecera del recibo de caja. Tipo: NUMERIC(20,5). Permite conversión de divisa en documentos multimoneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor del reintegro expresado en la moneda de la cabecera del recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyHeader';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario a reintegrar (devolver/reembolsar) del anticipo pagado. Tipo: DECIMAL(18,2). Monto principal del reembolso de adelanto.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'PaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se va a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'PaymentValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'PaymentValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del anticipo (adelanto/pago por anticipado) a reintegrar. Tipo: VARCHAR(20). Clave legible para búsqueda de anticipos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del anticipo que se reintegra (devuelve). Referencia a Payments.AdvancePayments. Vincula el reembolso al adelanto original.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anticipo que se va a reintegrar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del detalle de recibo de caja que ejecuta el reintegro del anticipo. Referencia a Treasury.CashReceiptDetails. Asocia la devolución al movimiento de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del recibo de caja que efectua el reintegro del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'CashReceiptDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de reintegro (devolución/reembolso) del anticipo. Tipo: INT IDENTITY. Clave primaria del movimiento de reembolso.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reintegro del anticipo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la relación entre los detalles de recibos de caja y los anticipos aplicados como forma de pago, indicando el valor del anticipo utilizado en cada transacción de cobro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashReceiptAdvancePayment';
