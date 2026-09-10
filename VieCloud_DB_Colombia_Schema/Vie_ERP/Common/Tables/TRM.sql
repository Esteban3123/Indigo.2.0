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
ALTER TABLE [Common].[TRM] NOCHECK CONSTRAINT [FK_TRM_Currency];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de cambio de la moneda oficial respecto a otra moneda (tipo de cambio directo). Ej: si moneda oficial es Dólar y moneda secundaria es Colón, guarda cuántos Colones equivalen a 1 Dólar (ej: 600). NUMERIC(20,5), FK a [Common].[Currency].', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de cambio de la moneda oficial respecto a la moneda  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 Dolar a cuantos Colones equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 600', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda oficial del sistema (referencia a divisa base). FK a [Common].[Currency], por defecto 1. Usado como denominador para cálculos de TRM.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda Oficial del Sistema', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de cambio inverso: cuánta moneda oficial equivale a 1 unidad de la moneda secundaria. Ej: si 1 Dólar = 600 Colones, este campo guarda 0.00166 (Dólares por Colón). NUMERIC(20,5).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la modena respecto a la modena oficial  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 colon a cuantos dolares equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 0,00166 son los dolares a lo que equivale 1 colon', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la moneda secundaria o alternativa para la cual se calcula el tipo de cambio. FK a [Common].[Currency]. Sinónimos: divisa, moneda, divisa transaccional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modena', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vigencia o registro de la tasa de cambio (TRM). Tipo DATE. Sinónimos: fecha de cotización, fecha de medición, fecha del tipo de cambio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'MeasurementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la medicion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'MeasurementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'MeasurementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria secuencial del registro de tasa de cambio (TRM). IDENTITY(1,1), INT. Identificador único e inmutable del movimiento de cambio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro de la tasa de cambio.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de cambio (TRM) por fecha: registra el valor de conversión entre monedas extranjeras y la moneda oficial para cada día, permitiendo calcular equivalencias en transacciones en divisas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'TRM';
