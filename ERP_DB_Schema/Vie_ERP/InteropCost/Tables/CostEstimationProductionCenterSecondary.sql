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
ALTER TABLE [InteropCost].[CostEstimationProductionCenterSecondary] NOCHECK CONSTRAINT [FK_CostEstimationProductionCenterSecondary_ProductionCenter];


GO
ALTER TABLE [InteropCost].[CostEstimationProductionCenterSecondary] NOCHECK CONSTRAINT [FK_CostEstimationProductionCenterSecondary_ProductionCenter1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID centro de producción del objetivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TargetProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distribución de transferencia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'TransferDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distribución de dispensación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DispensingDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distribución de activos fijos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'FixedAssetDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Administrativo y Los elementos del costo que sean de tipo mano de obra indirecta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionInDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de mano de obra del centro de produccion que sea de tipo Operativo y Los elementos del costo que sean de tipo mano de obra directa', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'ManPowerDistributionDirect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la distribucion de elementos del costo de tipo Otros Gastos con distribucion Calculada y Buscada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'AutoCostDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo directo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DirectCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de distribucion  1 - Directa  2 - Buscada   3 - Calculada', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'SourceProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'CostEstimationProductionCenterSecondary', @level2type = N'COLUMN', @level2name = N'Year';

