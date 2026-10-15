CREATE TABLE [Billing].[CustomTRM] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [OperatingUnitId]         INT             NOT NULL,
    [InitialMeasurementDate]  DATE            NOT NULL,
    [FinalMeasurementDate]    DATE            NOT NULL,
    [CurrencyId]              INT             NOT NULL,
    [Value]                   NUMERIC (20, 5) NOT NULL,
    [OfficialCurrencyId]      INT             NOT NULL,
    [ValueOfficialToCurrency] NUMERIC (20, 5) NOT NULL,
    CONSTRAINT [PK_TRM] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CustomTRM_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CustomTRM_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_CustomTRM2_Currency] FOREIGN KEY ([OfficialCurrencyId]) REFERENCES [Common].[Currency] ([Id])
);


GO
ALTER TABLE [Billing].[CustomTRM] NOCHECK CONSTRAINT [FK_CustomTRM_Currency];


GO
ALTER TABLE [Billing].[CustomTRM] NOCHECK CONSTRAINT [FK_CustomTRM_OperatingUnit];


GO
ALTER TABLE [Billing].[CustomTRM] NOCHECK CONSTRAINT [FK_CustomTRM2_Currency];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de cambio de la moneda oficial respecto a la moneda  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 Dolar a cuantos Colones equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 600', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Moneda Oficial del Sistema', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la modena respecto a la modena oficial  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 colon a cuantos dolares equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 0,00166 son los dolares a lo que equivale 1 colon', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modena', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Final de la Medición.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'FinalMeasurementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la medicion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'InitialMeasurementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Unidad Operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Autoincrementable del Registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Id';

