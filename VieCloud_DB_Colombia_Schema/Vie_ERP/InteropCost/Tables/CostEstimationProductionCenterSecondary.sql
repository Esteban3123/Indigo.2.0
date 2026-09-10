CREATE TABLE [InteropCost].[CostEstimationProductionCenterSecondary] (
    [Year]                         INT             CONSTRAINT [DF_CostEstimationProductionCenterSecondary_Year] DEFAULT ((0)) NOT NULL,
    [Month]                        INT             CONSTRAINT [DF_CostEstimationProductionCenterSecondary_Month] DEFAULT ((0)) NOT NULL,
    [SourceProductionCenterId]     INT             NOT NULL,
    [DistributionType]             TINYINT         CONSTRAINT [DF_TableProductionTmp_DistributionType] DEFAULT ((1)) NOT NULL,
    [DirectCost]                   NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_DirectCost] DEFAULT ((0)) NOT NULL,
    [AutoCostDistribution]         NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_AutoCostDistribution] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionDirect]   NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_ManPowerDistributionDirect] DEFAULT ((0)) NOT NULL,
    [ManPowerDistributionInDirect] NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_ManPowerDistributionInDirect] DEFAULT ((0)) NOT NULL,
    [FixedAssetDistribution]       NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_FixedAssetDistribution] DEFAULT ((0)) NOT NULL,
    [DispensingDistribution]       NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_DispensingDistribution] DEFAULT ((0)) NOT NULL,
    [TransferDistribution]         NUMERIC (18, 2) CONSTRAINT [DF_TableProductionTmp_TransferDistribution] DEFAULT ((0)) NOT NULL,
    [TargetProductionCenterId]     INT             NOT NULL,
    CONSTRAINT [FK_CostEstimationProductionCenterSecondary_ProductionCenter] FOREIGN KEY ([SourceProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id]),
    CONSTRAINT [FK_CostEstimationProductionCenterSecondary_ProductionCenter1] FOREIGN KEY ([TargetProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del centro de producción destino (FK InteropCost.ProductionCenter). Unidad funcional, servicio o área que recibe la distribución de costos secundarios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TargetProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID centro de producción del objetivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TargetProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TargetProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de transferencia (NUMERIC 18,2). Costo asignado por traslado, movimiento o referencia entre centros de producción; costo de transferencia de pacientes o recursos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distribución de transferencia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TransferDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de dispensación (NUMERIC 18,2). Costo de medicamentos, insumos o elementos dispensados desde farmacia u otro centro de suministro hacia unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distribución de dispensación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de activos fijos (NUMERIC 18,2). Costo de depreciación, mantenimiento y asignación de bienes inmuebles, equipos médicos e instalaciones.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distribución de activos fijos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de mano de obra indirecta (NUMERIC 18,2). Costo de personal administrativo, directivo y de soporte; elementos de costo de mano de obra indirecta del centro de producción.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Administrativo y Los elementos del costo que sean de tipo mano de obra indirecta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de mano de obra directa (NUMERIC 18,2). Costo de personal operativo y asistencial; elementos de costo de mano de obra directa relacionada con atención, procedimientos y diagnósticos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Operativo y Los elementos del costo que sean de tipo mano de obra directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución de otros gastos calculada (NUMERIC 18,2). Costo de elementos tipo otros gastos con distribución automática, calculada y buscada; gastos varios operacionales.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo directo (NUMERIC 18,2). Gasto atribuible directamente al centro de producción; costo de atención, procedimiento, servicio o unidad funcional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DirectCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo directo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DirectCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DirectCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución (TINYINT). Método de asignación: 1=Directa, 2=Buscada, 3=Calculada. Define cómo se distribuyen costos entre centros de producción.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de distribucion  1 - Directa  2 - Buscada   3 - Calculada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del centro de producción origen (FK InteropCost.ProductionCenter). Unidad funcional, servicio o área que origina la distribución de costos secundarios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'SourceProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'SourceProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'SourceProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes (INT, 1-12). Período mensual de estimación y distribución de costos por centro de producción.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año (INT). Período anual de estimación y distribución de costos por centro de producción; ciclo fiscal de análisis de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costos secundarios estimados entre centros de producción (centros de costo): registra cómo se distribuyen los costos de un centro de producción origen hacia un centro de producción destino, desglosados por tipo de distribución (mano de obra directa e indirecta, activos fijos, dispensación, traslados y distribución automática), para un período mensual y anual determinado. Se usa en el módulo de costeo hospitalario para la asignación indirecta de costos entre unidades funcionales o servicios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary';
