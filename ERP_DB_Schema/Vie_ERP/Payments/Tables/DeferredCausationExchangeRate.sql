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
ALTER TABLE [Payments].[DeferredCausationExchangeRate] NOCHECK CONSTRAINT [FK_DeferredCausationExchangeRate_AccountPayable];


GO
ALTER TABLE [Payments].[DeferredCausationExchangeRate] NOCHECK CONSTRAINT [FK_DeferredCausationExchangeRate_Currency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la tasa de cambio de la moneda de esta tabla (CurrencyId) contra la moneda de la cxp de diferido   Ejemplo CxP en dolares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo    Ejemplo 2  Ejemplo Cxp en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'ValueReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la tasa de cambio de la moneda del anticipo contra la moneda de esta tabla (CurrencyId)  Ejemplo CxC en dollares, moneda de esta tabla Colones (1  dolar = 650 Colones)  En ese escenario se guardaria 650 en este campo    Ejemplo 2  Ejemplo CxC en Colones, moneda de esta tabla Dolares (1  dolar = 650 Colones)  En ese escenario se guardaria 0,00153 en este campo  ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'DeferredCausationExchangeRate', @level2type = N'COLUMN', @level2name = N'Value';

