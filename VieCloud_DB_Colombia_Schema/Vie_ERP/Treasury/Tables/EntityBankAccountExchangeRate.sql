CREATE TABLE [Treasury].[EntityBankAccountExchangeRate] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [EntityBankAccountId] INT             NOT NULL,
    [CurrencyId]          INT             NOT NULL,
    [Value]               NUMERIC (20, 5) NOT NULL,
    [ValueReverse]        NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_EntityBankAccountExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntityBankAccountExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_EntityBankAccountExchangeRate_EntityBankAccount] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa (NUMERIC 20,5): CurrencyId → moneda de la Cuenta Bancaria. Recíproco de Value para conversión bidireccional. Ej: Cuenta USD, CurrencyId=Colones → ValueReverse=0.00153. Ej: Cuenta Colones, CurrencyId=USD → ValueReverse=650. Usado para reconciliación y reportes multidivisa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la Cuenta bancaria   Ejemplo Cuenta bancaria en dolares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Cuenta bancaria en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio (NUMERIC 20,5): moneda de la Cuenta Bancaria → CurrencyId. Ej: Cuenta en USD, CurrencyId=Colones → Value=650. Ej: Cuenta en Colones, CurrencyId=USD → Value=0.00153. Convierte saldo de la cuenta a la moneda objetivo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la tasa de cambio de la moneda de la Cuenta bancaria contra la moneda de esta tabla (CurrencyId)  Ejemplo Caja en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo Cuenta bancaria en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la Moneda contra la cual se calcula el tipo de cambio. Referencia a Common.Currency. Representa la divisa de conversión (ej: dólar, colón, euro).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la Cuenta Bancaria de la Entidad. Referencia a Treasury.EntityBankAccounts. Vincula la tasa de cambio a una cuenta específica (ej: cuenta en dólares, colones, euros).', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad de la cuenta bancaria.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de tasa de cambio. Clave primaria que referencia el histórico de tasas de cambio entre monedas y cuentas bancarias.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de cambio asociadas a cuentas bancarias de entidades, registrando el valor de conversión entre monedas para cada cuenta bancaria y divisa configurada.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountExchangeRate';
