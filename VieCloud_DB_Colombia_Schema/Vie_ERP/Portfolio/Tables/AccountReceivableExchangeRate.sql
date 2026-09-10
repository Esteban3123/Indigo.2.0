CREATE TABLE [Portfolio].[AccountReceivableExchangeRate] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [AccountReceivableId] INT             NOT NULL,
    [CurrencyId]          INT             NOT NULL,
    [Value]               NUMERIC (20, 5) NOT NULL,
    [ValueReverse]        NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_AccountReceivableExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableExchangeRate_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivableExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [IX_AccountReceivableExchangeRate] UNIQUE NONCLUSTERED ([AccountReceivableId] ASC, [CurrencyId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa: de la moneda de esta tabla (CurrencyId) hacia la moneda de la CxC. Ejemplo: CxC en dólares, tabla en colones (1 USD=650 CRC) → guarda 0.00153. Tipo: NUMERIC(20,5). Sinónimos: tipo de cambio inverso, cotización recíproca.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la CxC   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio de la moneda de la CxC hacia la moneda de esta tabla (CurrencyId). Ejemplo: CxC en dólares, tabla en colones (1 USD=650 CRC) → guarda 650. Tipo: NUMERIC(20,5). Sinónimos: tipo de cambio directo, cotización.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de la CxC contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Moneda oficial de conversión; referencia FK a Common.Currency. Sinónimos: divisa, moneda extranjera, código de moneda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Cuenta por Cobrar (CxC) relacionada; referencia FK a Portfolio.AccountReceivable. Sinónimos: factura por cobrar, deuda, ingreso pendiente de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de tasa de cambio en cuentas por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de cambio de moneda aplicadas a las cuentas por cobrar. Registra el valor de conversión y su inverso para cada combinación de cuenta por cobrar y divisa, permitiendo gestionar cartera en múltiples monedas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate';
