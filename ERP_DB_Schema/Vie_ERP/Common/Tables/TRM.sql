CREATE TABLE [Common].[TRM] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [MeasurementDate]         DATE            NOT NULL,
    [CurrencyId]              INT             NOT NULL,
    [Value]                   NUMERIC (20, 5) NOT NULL,
    [OfficialCurrencyId]      INT             CONSTRAINT [DF__TRM__OfficialCur__2DC30F9E] DEFAULT ((1)) NOT NULL,
    [ValueOfficialToCurrency] NUMERIC (20, 5) CONSTRAINT [DF_TRM_ValueOfficalToCurrency] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TRM] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TRM_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_TRM2_Currency] FOREIGN KEY ([OfficialCurrencyId]) REFERENCES [Common].[Currency] ([Id])
);


GO
ALTER TABLE [Common].[TRM] NOCHECK CONSTRAINT [FK_TRM_Currency];


GO
ALTER TABLE [Common].[TRM] NOCHECK CONSTRAINT [FK_TRM2_Currency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de cambio de la moneda oficial respecto a la moneda  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 Dolar a cuantos Colones equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 600', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda Oficial del Sistema', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la modena respecto a la modena oficial  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 colon a cuantos dolares equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 0,00166 son los dolares a lo que equivale 1 colon', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modena', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la medicion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'MeasurementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de la tasa de cambio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Id';

