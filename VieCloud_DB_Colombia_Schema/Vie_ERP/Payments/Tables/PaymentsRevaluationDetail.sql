CREATE TABLE [Payments].[PaymentsRevaluationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [PaymentsRevaluationId]      INT             NOT NULL,
    [DocumentType]               TINYINT         CONSTRAINT [DF_PaymentsRevaluationDetail_DocumentType] DEFAULT ((1)) NOT NULL,
    [AccountPayableId]           INT             NULL,
    [AdvancePaymentId]           INT             NULL,
    [ThirdPartyId]               INT             NULL,
    [CostCenterId]               INT             NULL,
    [Value]                      NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) NOT NULL,
    [CurrencyId]                 INT             NOT NULL,
    [CurrencyConverterId]        INT             NOT NULL,
    [ValueCurrency]              NUMERIC (20, 5) NOT NULL,
    [ValueCurrencyReverse]       NUMERIC (20, 5) NOT NULL,
    [ActualValueCurrency]        NUMERIC (20, 5) NOT NULL,
    [ActualValueCurrencyReverse] NUMERIC (20, 5) NOT NULL,
    [MainAccountId]              INT             NOT NULL,
    [LegalBookId]                INT             NOT NULL,
    [BalanceConverted]           NUMERIC (18, 2) NOT NULL,
    [ActualBalanceConverter]     NUMERIC (18, 2) NOT NULL,
    [ProfitLostValue]            NUMERIC (18, 2) CONSTRAINT [DF_PaymentsRevaluationDetail_ProfitLostValue] DEFAULT ((0)) NOT NULL,
    [DeferredCausationId]        INT             NULL,
    CONSTRAINT [PK_RevaluationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentsRevaluation_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluation_AdvancePayments] FOREIGN KEY ([AdvancePaymentId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_CurrencyConverter] FOREIGN KEY ([CurrencyConverterId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_DeferredCausation] FOREIGN KEY ([DeferredCausationId]) REFERENCES [Payments].[DeferredCausation] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_PaymentsRevaluation] FOREIGN KEY ([PaymentsRevaluationId]) REFERENCES [Payments].[PaymentsRevaluation] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con documento de causación diferida de CxP (Cuentas por Pagar), FK a [Payments].[DeferredCausation]. Vincula gastos devengados no pagados inmediatamente.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el documento de causacion diferida de cxp', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de ganancia o pérdida (NUMERIC 18,2) generado por diferencia de cambio en revaluación de pagos. Default 0.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor perdido de ganancias', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo convertido actual (NUMERIC 18,2) en moneda destino post-revaluación, resultado de aplicar tasa de cambio real.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convertidor de saldo actual', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance convertido (NUMERIC 18,2) en moneda de conversión tras aplicar tasa de cambio inicial de revaluación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance convertido', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Libro Legal o Libro Contable (FK a [GeneralLedger].[LegalBook]) donde se registra la transacción contable.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro legal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta Contable Principal o Cuenta Mayor (FK a [GeneralLedger].[MainAccounts]) asociada a la revaluación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real en moneda inversa o contraparte (NUMERIC 20,5) tras actualización de tasa de cambio final.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor real Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real en moneda de conversión (NUMERIC 20,5) después de aplicar tasa de cambio actual o actualizada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda de valor real', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en moneda inversa o contraparte (NUMERIC 20,5) según tasa de cambio inicial de revaluación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor convertido en moneda destino (NUMERIC 20,5) aplicando tasa de cambio de revaluación inicial.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Moneda Convertida o Destino (FK a [Common].[Currency]), moneda a la cual se convierte el valor.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del conversor de moneda ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Moneda Original o Base (FK a [Common].[Currency]) del monto en revaluación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance o saldo pendiente (NUMERIC 18,2) en moneda original de la obligación de pago revaluada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o monto principal (NUMERIC 18,2) en moneda original sujeto a revaluación por diferencia de cambio.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Centro de Costos (FK a [Payroll].[CostCenter]) al cual se imputa el gasto o pago revaluado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Tercero, Proveedor o Acreedor (FK a [Common].[ThirdParty]) involucrado en la obligación de pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Pago por Adelantado o Anticipo de CxP (FK a [Payments].[AdvancePayments]) relacionado con la revaluación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del pago por adelantado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Cuenta por Pagar (FK a [Payments].[AccountPayable]) origen de la obligación revaluada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento (TINYINT, 1=CxP/Factura, 2=Anticipo CxP, 3=Diferido). Clasifica la naturaleza de la obligación revaluada.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de documento   1 - Cuentas por Pagar   2 - Anticipos de CxP. 
3 - Diferidos ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de Revaluación de Pagos (FK a [Payments].[PaymentsRevaluation]), encabezado que agrupa los detalles de revaluación.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'PaymentsRevaluationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la revaluación de pagos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'PaymentsRevaluationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'PaymentsRevaluationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) del detalle de revaluación de pagos, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la revaluación de pagos en moneda extranjera: registra línea por línea el impacto contable de actualizar saldos de cuentas por pagar a la tasa de cambio vigente, calculando diferencias en cambio (ganancias o pérdidas) por cada documento, tercero y centro de costo involucrado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail';
