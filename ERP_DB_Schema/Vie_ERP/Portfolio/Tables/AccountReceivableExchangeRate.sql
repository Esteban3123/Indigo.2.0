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
ALTER TABLE [Portfolio].[AccountReceivableExchangeRate] NOCHECK CONSTRAINT [FK_AccountReceivableExchangeRate_AccountReceivable];


GO
ALTER TABLE [Portfolio].[AccountReceivableExchangeRate] NOCHECK CONSTRAINT [FK_AccountReceivableExchangeRate_Currency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la CxC   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la tasa de cambio de la moneda de la CxC contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda oficial.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por cobrar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';

