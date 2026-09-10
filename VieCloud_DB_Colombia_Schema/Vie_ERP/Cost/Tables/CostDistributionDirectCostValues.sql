CREATE TABLE [Cost].[CostDistributionDirectCostValues] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [ByArea]                   DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByArea] DEFAULT ((0)) NOT NULL,
    [ByOfficialHours]          DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByOfficialHours] DEFAULT ((0)) NOT NULL,
    [BySupplyValue]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_BySupplyValue] DEFAULT ((0)) NOT NULL,
    [ByWorkmanship]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByWorkmanship] DEFAULT ((0)) NOT NULL,
    [ByAssetValue]             DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_ByAssetValue] DEFAULT ((0)) NOT NULL,
    [BySales]                  DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionDirectCostValues_BySales] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostValues_CostDistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostValues_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo directo distribuido por ventas; monto (DECIMAL 18,2) asignado según volumen o ingresos de venta del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ventas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo directo distribuido por activos; monto (DECIMAL 18,2) prorrateado según valor de bienes/equipos del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de activo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo directo distribuido por mano de obra; monto (DECIMAL 18,2) asignado según salarios, honorarios o recursos humanos del centro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo directo distribuido por suministros; monto (DECIMAL 18,2) prorrateable según insumos, materiales y supplies utilizados', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor de oferta', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo directo distribuido por horas oficiales; monto (DECIMAL 18,2) asignado según jornadas de operación o tiempo productivo del centro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'horas oficiales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de costo directo distribuido por área; monto (DECIMAL 18,2) prorrateable según superficie o espacio físico del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Area', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Centro de Producción; identificador único (INT) del centro funcional, unidad operativa o área de servicio a la que se asigna el costo directo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a Distribución de Costo Directo; identificador único (INT) del registro de asignación de costos directos que se detalla en esta tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'D de costo directo de distribución', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT, PK); clave primaria del registro de valor de distribución de costo directo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores calculados de distribución de costos directos por centro de producción. Registra cuánto del costo directo corresponde a cada centro según distintos criterios de distribución: área física, horas oficiales, insumos, mano de obra, activos y ventas.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostValues';
