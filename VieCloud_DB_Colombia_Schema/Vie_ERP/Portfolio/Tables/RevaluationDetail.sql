CREATE TABLE [Portfolio].[RevaluationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [RevaluationId]              INT             NOT NULL,
    [DocumentType]               TINYINT         CONSTRAINT [DF_RevaluationDetail_DocumentType] DEFAULT ((1)) NOT NULL,
    [PortfolioAdvanceId]         INT             NULL,
    [AccountReceivableId]        INT             NULL,
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
    [ProfitLostValue]            NUMERIC (18, 2) CONSTRAINT [DF_RevaluationDetail_ProfitLostValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RevaluationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevaluationDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_CurrencyConverter] FOREIGN KEY ([CurrencyConverterId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_Revaluation] FOREIGN KEY ([RevaluationId]) REFERENCES [Portfolio].[Revaluation] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de ganancia o pérdida generado por revalorización (NUMERIC 18,2), diferencial contable por ajuste de tipo de cambio o actualización de valor de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor perdido de ganancias', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo actual convertido a moneda base aplicando tasa de cambio real (NUMERIC 18,2), usado en conciliación de cartera multicurrency', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convertidor de saldo actual', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo convertido a moneda de reporte desde moneda original (NUMERIC 18,2), refleja el saldo después de aplicar conversión de divisa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convertir saldo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro legal (FK a GeneralLedger.LegalBook), referencia al libro contable que registra la revalorización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro legal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (FK a GeneralLedger.MainAccounts), código de la cuenta donde se registra el ajuste contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real en moneda inversa/contraparte (NUMERIC 20,5), monto actualizado en divisa de reversión para análisis bidireccional', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor real Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real actual en moneda extranjera (NUMERIC 20,5), importe actualizado después de revalorización en divisa original', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda valor real', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en moneda inversa/contraparte inicial (NUMERIC 20,5), monto en divisa de reversión antes de actualización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en moneda extranjera (NUMERIC 20,5), importe original antes de revalorización en divisa diferente a base', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la moneda  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del conversor de divisa destino (FK a Common.Currency), moneda de conversión/reporte para cambio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Convertidor de divisas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda origen (FK a Common.Currency), divisa en que está expresado el documento o transacción original', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo no convertido en moneda original (NUMERIC 18,2), monto pendiente antes de aplicar ajustes de revalorización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Balance', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base sin convertir (NUMERIC 18,2), monto principal del documento antes de cambios de divisa o revalorización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos (FK a Payroll.CostCenter), unidad funcional o departamento que generó el documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de costos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/entidad (FK a Common.ThirdParty), cliente, proveedor o deudor relacionado con la cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar (FK a Portfolio.AccountReceivable), factura o deuda a revaluar si aplica', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del anticipo de cartera (FK a Portfolio.PortfolioAdvance), advance o pago anticipado si es tipo documento 2', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Avance de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento revaluado (TINYINT, 1=Cuentas por Cobrar, 2=Anticipos), clasifica si es deuda o anticipo recibido', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de documento   1 - Cuentas por cobrar  2 - Anticipos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la revalorización padre (FK a Portfolio.Revaluation), proceso de actualización que contiene este detalle', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'RevaluationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Revalorización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'RevaluationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'RevaluationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY), clave primaria de detalle de revalorización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las revaluaciones de cartera: registra línea a línea los documentos (anticipos o cuentas por cobrar) afectados por un proceso de revaluación de moneda extranjera, con los valores originales, convertidos y la ganancia o pérdida generada por diferencia en cambio.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail';
