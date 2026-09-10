CREATE TABLE [Treasury].[CashRegisterExchangeRate] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [CashRegisterId] INT             NOT NULL,
    [CurrencyId]     INT             NOT NULL,
    [Value]          NUMERIC (20, 5) NOT NULL,
    [ValueReverse]   NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_CashRegisterExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashRegisterExchangeRate_CashRegister] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_CashRegisterExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa: CurrencyId → moneda de caja. NUMERIC(20,5). Ej: caja USD, tabla COP = 0.00024 (1 COP = 0.00024 USD). Recíproco de Value.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la Caja   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio directa: moneda de caja → CurrencyId. NUMERIC(20,5). Ej: caja USD, tabla COP = 4100 (1 USD = 4100 COP).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de la Caja contra la moneda de esta tabla (CurrencyId)  Ejemplo Caja en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda destino (FK → Common.Currency). Moneda contra la cual se calcula la tasa de cambio de la caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la caja registradora (FK → Treasury.CashRegisters). Relaciona la tasa de cambio con su caja de origen.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro de caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de tasa de cambio en caja.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de cambio de divisas configuradas por caja registradora. Permite manejar múltiples monedas en cada punto de caja, registrando el valor de conversión y su inverso para operar transacciones en diferentes monedas.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashRegisterExchangeRate';
