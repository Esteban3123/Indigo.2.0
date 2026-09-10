CREATE TABLE [Payments].[DeferredCausationExchangeRate] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [DeferredCausationId] INT             NOT NULL,
    [CurrencyId]          INT             NOT NULL,
    [Value]               NUMERIC (20, 5) NOT NULL,
    [ValueReverse]        NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_DeferredCausationExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeferredCausationExchangeRate_AccountPayable] FOREIGN KEY ([DeferredCausationId]) REFERENCES [Payments].[DeferredCausation] ([Id]),
    CONSTRAINT [FK_DeferredCausationExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [IX_DeferredCausationExchangeRate] UNIQUE NONCLUSTERED ([DeferredCausationId] ASC, [CurrencyId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa (recíproca) de la moneda de CxP diferida hacia la moneda de esta tabla (CurrencyId). Tipo: NUMERIC(20,5). Si CxP está en dólares y tabla en colones (1 USD=650 CRC), se guarda 0.00153 (inversa). Si CxP en colones y tabla en dólares, se guarda 650. Usado para conversión de cuentas por pagar diferidas en pagos con múltiples monedas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la cxp de diferido   Ejemplo CxP en dolares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Cxp en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio directa de la moneda del diferido contra la moneda de esta tabla (CurrencyId). Tipo: NUMERIC(20,5). Si diferido en dólares y tabla en colones (1 USD=650 CRC), se guarda 650. Si diferido en colones y tabla en dólares, se guarda 0.00153. Referencia FK a [Payments].[DeferredCausation] e [Common].[Currency]. Utilizado en conversión de moneda para cuentas por pagar diferidas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda del anticipo contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dollares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de cambio asociadas a causaciones diferidas de pagos. Registra el valor de conversión entre monedas para cada causación diferida, permitiendo calcular equivalencias en distintas divisas al momento del pago.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de tasa de cambio para la causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la causación diferida de pago a la que pertenece esta tasa de cambio.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Moneda utilizada en la conversión; identifica la divisa (por ejemplo: peso colombiano, dólar, euro) aplicada a la causación diferida.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
