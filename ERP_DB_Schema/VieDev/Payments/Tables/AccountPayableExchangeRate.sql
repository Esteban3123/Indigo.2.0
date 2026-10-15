CREATE TABLE [Payments].[AccountPayableExchangeRate] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [AccountPayableId] INT             NOT NULL,
    [CurrencyId]       INT             NOT NULL,
    [Value]            NUMERIC (20, 5) NOT NULL,
    [ValueReverse]     NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_AccountPayableExchangeRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableExchangeRate_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_AccountPayableExchangeRate_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [IX_AccountPayableExchangeRate] UNIQUE NONCLUSTERED ([AccountPayableId] ASC, [CurrencyId] ASC)
);


GO
ALTER TABLE [Payments].[AccountPayableExchangeRate] NOCHECK CONSTRAINT [FK_AccountPayableExchangeRate_AccountPayable];


GO
ALTER TABLE [Payments].[AccountPayableExchangeRate] NOCHECK CONSTRAINT [FK_AccountPayableExchangeRate_Currency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la CxC   Ejemplo CxC en dolates, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la tasa de cambio de la moneda del anticipo contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dollares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por pagar ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableExchangeRate', @level2type = N'COLUMN', @level2name = N'Id';

