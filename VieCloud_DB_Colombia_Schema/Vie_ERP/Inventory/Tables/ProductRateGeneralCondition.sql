CREATE TABLE [Inventory].[ProductRateGeneralCondition] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [ProductRateGeneralId] INT             NOT NULL,
    [InitialValue]         DECIMAL (18, 2) NULL,
    [EndValue]             DECIMAL (18, 2) NULL,
    [SalesValue]           NUMERIC (18, 2) CONSTRAINT [DF_ProductRateGeneralCondition_SalesValue] DEFAULT ((0)) NOT NULL,
    [Percentage]           NUMERIC (6, 2)  NULL,
    CONSTRAINT [PK_ProductRateGeneralCondition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductRateGeneralCondition_ProductRateGeneral] FOREIGN KEY ([ProductRateGeneralId]) REFERENCES [Inventory].[ProductRateGeneral] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento o recargo aplicable a la tarifa (NUMERIC 6,2). Se completa solo cuando la regla de fijación de precio es por porcentaje, no por valor fijo. Búsqueda: porcentaje, descuento, recargo, ajuste porcentual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del porcentaje, este valor solo se solicita si la regla es por porcentaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de venta fijo del producto en moneda local (NUMERIC 18,2, default=0). Se completa únicamente cuando la tarifa es fija (no porcentual). Búsqueda: precio fijo, valor de venta, tarifa fija, costo producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del producto, solo se llena con un valor diferente a cero si es tarifa fija', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo o límite superior del rango de condición (DECIMAL 18,2). Se completa solo si la condición aplica por rango de valor total (ConditionType=2). Búsqueda: valor final, límite superior, rango máximo, techo valor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'EndValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Final, solo se llena si la condicion es por valor total (ConditionType 2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'EndValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'EndValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo o límite inferior del rango de condición (DECIMAL 18,2). Se completa solo si la condición aplica por rango de valor total (ConditionType=2). Búsqueda: valor inicial, límite inferior, rango mínimo, piso valor.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial, solo se llena si la condicion es por valor total (ConditionType 2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la tarifa general del producto (INT, NOT NULL). Vincula a tabla ProductRateGeneral. Búsqueda: tarifa producto, identificación tarifa, relación producto-precio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'ProductRateGeneralId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación general de la tarifa del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'ProductRateGeneralId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'ProductRateGeneralId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la condición de tarifa del producto (INT IDENTITY, PK). Clave primaria de la tabla. Búsqueda: identificación, registro, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones de tarifas generales por producto en inventario. Define los rangos de valores y porcentajes aplicables a cada tarifa general, permitiendo establecer tramos de precio o descuento según el valor inicial, valor final y precio de venta asociado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateGeneralCondition';
