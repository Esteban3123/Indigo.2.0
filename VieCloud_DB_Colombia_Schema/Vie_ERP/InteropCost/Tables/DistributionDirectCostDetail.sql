CREATE TABLE [InteropCost].[DistributionDirectCostDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [MainAccountId]            INT             NOT NULL,
    [CostCenterId]             INT             NULL,
    [MeasurementUnitId]        INT             NULL,
    [Percentage]               NUMERIC (5, 2)  CONSTRAINT [DF_DistributionDirectCostDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Count]                    NUMERIC (18, 2) CONSTRAINT [DF_DistributionDirectCostDetail_Count] DEFAULT ((0)) NOT NULL,
    [CostValue]                DECIMAL (18, 2) CONSTRAINT [DF_DistributionDirectCostDetail_CostValue] DEFAULT ((0)) NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionDirectCostDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionDirectCostDetail_DistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [InteropCost].[DistributionDirectCost] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_DistributionDirectCostDetail__ProductionCenterId__MeasurementUnitId__DistributionDirectCostId__CostValue]
    ON [InteropCost].[DistributionDirectCostDetail]([ProductionCenterId] ASC, [MeasurementUnitId] ASC, [DistributionDirectCostId] ASC, [CostValue] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de la distribución de gasto directo (NUMERIC 18,2). Monto base antes de aplicar porcentaje o cálculo de costo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo calculado en DECIMAL (18,2). Resultado final del costo asignado al detalle tras distribución, por defecto 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del costo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o conteo de unidades (NUMERIC 18,2). Volumen de medida asociado al gasto directo, por defecto 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Count';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Count';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de asignación al centro de costo (NUMERIC 5,2). Proporción % que corresponde al centro tras la distribución directa, rango 0-100, por defecto 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje que le corresponde al centro de costo despues de realizar laq distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida, restricción tipo 4 (sistema de costos). Referencia a InventoryMeasurementUnit para normalizar mediciones en cálculo de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costos (INT, nullable). Centro receptor o responsable del costo distribuido en la estructura contable.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id centro de costos ', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal (INT). Cuenta mayor o rubro contable donde se registra el gasto directo distribuido.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción (INT, FK). Unidad funcional o área productiva origen del gasto directo a distribuir.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de distribución (INT, FK). Referencia al documento padre que agrupa este detalle de gasto directo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion por gasto directo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de distribución (INT, PK Identity). Clave primaria que identifica cada línea de distribución de gasto directo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion por gasto directo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución de costos directos por centro de producción: registra el desglose de cada distribución de costo directo, indicando el centro de producción, cuenta contable principal, centro de costo, unidad de medida, porcentaje asignado, cantidad y valor del costo correspondiente.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostDetail';
