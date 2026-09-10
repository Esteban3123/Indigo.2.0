CREATE TABLE [Payments].[AdvancePaymentsExchangeRate] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [AdvancePaymentsId] INT             NOT NULL,
    [CurrencyId]        INT             NOT NULL,
    [Value]             NUMERIC (20, 5) NOT NULL,
    [ValueReverse]      NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_AccountReceivableExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdvancePaymentsExchangeRate_AdvancePayments] FOREIGN KEY ([AdvancePaymentsId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_AdvancePaymentsExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [IX_AdvancePaymentsExchangeRate] UNIQUE NONCLUSTERED ([AdvancePaymentsId] ASC, [CurrencyId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa (reverse exchange rate) de la moneda de esta tabla (CurrencyId) hacia la moneda del anticipo. Valor numérico con 5 decimales. Reciprocal o inversa matemática de Value. Ejemplo: USD en CxC con moneda CRC se guarda 0.00153; CRC en CxC con moneda USD se guarda 650.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la CxC   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio (exchange rate) de la moneda del anticipo hacia la moneda de esta tabla (CurrencyId). Valor numérico con 5 decimales. Ejemplo: USD a CRC 1 dólar = 650 colones se guarda como 650; CRC a USD 1 colón = 0.00153 dólares se guarda como 0.00153.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda del anticipo contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dollares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda de conversión, equivalente a divisa, coin o currency. Referencia foránea a Common.Currency(Id). Define la moneda destino contra la cual se calcula la tasa de cambio del anticipo.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del pago por adelantado, anticipo o prepago asociado. Referencia foránea a Payments.AdvancePayments(Id). Vincula la tasa de cambio al registro de anticipo específico.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del pago por adelantado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'AdvancePaymentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la fila de tasa de cambio para anticipos. Clave primaria de la tabla AdvancePaymentsExchangeRate.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio aplicada a los anticipos de pago. Registra el valor de conversión entre monedas para cada anticipo, permitiendo calcular el equivalente en moneda extranjera y su reverso.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AdvancePaymentsExchangeRate';
