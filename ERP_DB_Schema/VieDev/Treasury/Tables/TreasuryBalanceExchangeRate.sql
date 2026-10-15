CREATE TABLE [Treasury].[TreasuryBalanceExchangeRate] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [TreasuryBalanceId] INT             NOT NULL,
    [CurrencyId]        INT             NOT NULL,
    [Value]             NUMERIC (20, 5) NOT NULL,
    [ValueReverse]      NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_TreasuryBalanceExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la Caja   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalanceExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la tasa de cambio de la moneda de la Caja contra la moneda de esta tabla (CurrencyId)  Ejemplo Caja en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo Caja en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalanceExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del saldo de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalanceExchangeRate', @level2type = N'COLUMN', @level2name = N'TreasuryBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalanceExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';

