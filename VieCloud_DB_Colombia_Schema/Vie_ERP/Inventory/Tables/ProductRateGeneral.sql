CREATE TABLE [Inventory].[ProductRateGeneral] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ProductRateId]     INT             NOT NULL,
    [RuleType]          TINYINT         NOT NULL,
    [ProductTypeId]     INT             NULL,
    [ProductGroupId]    INT             NULL,
    [ProductSubGroupId] INT             NULL,
    [InitialDate]       DATETIME        NOT NULL,
    [EndDate]           DATETIME        NOT NULL,
    [RateType]          TINYINT         NOT NULL,
    [PercentageBasedOn] TINYINT         NOT NULL,
    [ConditionType]     TINYINT         NOT NULL,
    [Percentage]        NUMERIC (6, 2)  NULL,
    [SalesValue]        NUMERIC (18, 2) CONSTRAINT [DF_ProductRateGeneral_SalesValue] DEFAULT ((0)) NOT NULL,
    [Observations]      VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ProductRateGeneral] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateGeneral_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_ProductRateGeneral_ProductRateId] FOREIGN KEY ([ProductRateId]) REFERENCES [Inventory].[ProductRate] ([Id]),
    CONSTRAINT [FK_ProductRateGeneral_ProductSubGroup] FOREIGN KEY ([ProductSubGroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_ProductRateGeneral_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas y comentarios adicionales sobre la tarifa general del producto, anotaciones libres', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de venta del producto en tarifa fija (NUMERIC 18,2), se completa solo cuando RateType=1 y corresponde al precio fijo de comercialización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto, solo se llena con un valor diferente a cero si es tarifa fija', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de ajuste o margen (NUMERIC 6,2), válido solo si RateType=2 (tarifa basada en porcentaje) y ConditionType=1 (ninguna condición)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje, este valor solo se solicita si RateType es tipo 2 y la condicion es ninguna (1)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de condición para cálculo de precio: 1=Ninguna, 2=Valor Total, determina si aplica condición sobre variable base', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de condicion para calcular el precio del producto  1  - Ninguna  2 - Valor Total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variable base para cálculo de porcentaje: 0=N/A, 1=Costo promedio ponderado, 2=Último costo adquisición, referencia para margen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica en que variable esta basado el porcentaje  0 - N/A  1 - Costo promedio ponderado  2 - Ultimo costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'PercentageBasedOn';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tarifa: 1=Tarifa Fija (SalesValue), 2=Basada en Porcentaje, método de cálculo del precio final del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de tarifa  1 - Tarifa Fija  2 - Basado en porcentaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de vigencia de la tarifa (DATETIME), define hasta cuándo aplica esta configuración de precios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de la tarifa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de vigencia de la tarifa (DATETIME), define desde cuándo aplica esta configuración de precios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de la tarifa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del subgrupo de producto (INT, FK → ProductSubGroup.Id), permite agrupar tarifas por clasificación detallada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del SubGrupo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductSubGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de producto (INT, FK → ProductGroup.Id), permite agrupar tarifas por categoría de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de producto (INT, FK → ProductType.Id), permite agrupar tarifas por tipo específico de artículo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de regla de agrupación: 1=Por Tipo Producto, 2=Por Grupo Producto, 3=Por Subgrupo Producto, nivel de granularidad de aplicación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RuleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de regla para la agrupacion de los productos  1 - Tipo Producto  2 - Grupo Producto  3 - Subgrupo Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RuleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'RuleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tarifa del producto (INT, FK → ProductRate.Id), relación con tarifa padre o maestra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tarifa del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'ProductRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY), clave primaria de la tabla ProductRateGeneral', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reglas generales de tarifas para productos del inventario. Define condiciones de precios, porcentajes y valores de venta aplicables a tipos, grupos o subgrupos de productos dentro de un rango de fechas vigente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneral';
