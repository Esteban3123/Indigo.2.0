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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio (tipo de cambio, TRM) de la moneda oficial hacia la moneda secundaria. Valor numérico NUMERIC(20,5) que expresa cuántas unidades de moneda secundaria equivalen a 1 unidad de moneda oficial. Ejemplo: si moneda oficial es USD y secundaria COP, valor 4000 indica 1 USD = 4000 COP. Usado para conversión de facturas y RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de cambio de la moneda oficial respecto a la moneda  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 Dolar a cuantos Colones equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 600', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'ValueOfficialToCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero (FK a Common.Currency) de la moneda oficial del sistema ERP/EHR a nivel de unidad operativa. Moneda base o de referencia para todas las transacciones y facturación de la unidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Moneda Oficial del Sistema', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OfficialCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio inversa: valor NUMERIC(20,5) que expresa cuántas unidades de moneda oficial equivalen a 1 unidad de moneda secundaria (CurrencyId). Ejemplo: si 1 USD = 4000 COP, entonces 1 COP = 0.00025 USD. Complemento de ValueOfficialToCurrency para conversiones bidireccionales en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la modena respecto a la modena oficial  Ejemplo Moneda Oficial Dolar, Moneda: Colon   En este caso se guardaria 1 colon a cuantos dolares equivale (si un dolar valiera 600 colones)  entonces en este campo se guardara 0,00166 son los dolares a lo que equivale 1 colon', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero (FK a Common.Currency) de la moneda secundaria o alternativa respecto a la oficial. Moneda para la cual se registra el tipo de cambio en el período de medición. Usado en facturas, glosas y reportes RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modena', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de término (tipo DATE) del período de vigencia del tipo de cambio (TRM) registrado. Marca fin de validez de la tasa de conversión entre monedas para la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'FinalMeasurementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Medición.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'FinalMeasurementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'FinalMeasurementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio (tipo DATE) del período de vigencia del tipo de cambio (TRM) registrado. Marca desde cuándo es válida la tasa de conversión entre monedas para facturación y auditoría RIPS en la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'InitialMeasurementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la medicion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'InitialMeasurementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'InitialMeasurementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero (FK a Common.OperatingUnit) de la unidad operativa, centro de atención o unidad funcional a la que aplica este tipo de cambio. Permite TRM diferenciados por sede o contrato.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Operativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria INT IDENTITY(1,1). Identificador único autoincrementable del registro de tipo de cambio (TRM) en CustomTRM. Referencia para auditoría de conversiones de moneda en facturas y RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador Autoincrementable del Registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasas de cambio personalizadas (TRM) por unidad operativa y moneda, usadas en facturación para convertir valores entre monedas extranjeras y la moneda oficial en un rango de fechas determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'CustomTRM';
