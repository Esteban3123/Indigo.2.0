CREATE TABLE [InteropCost].[DistributionDirectCostValues] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [ByArea]                   DECIMAL (18, 2) CONSTRAINT [DF_DistributionDirectCostValues_ByArea] DEFAULT ((0)) NOT NULL,
    [ByOfficialHours]          DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea1] DEFAULT ((0)) NOT NULL,
    [BySupplyValue]            DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea2] DEFAULT ((0)) NOT NULL,
    [ByWorkmanship]            DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea3] DEFAULT ((0)) NOT NULL,
    [ByAssetValue]             DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea4] DEFAULT ((0)) NOT NULL,
    [BySales]                  DECIMAL (18, 2) CONSTRAINT [DF_Table_1_ByArea5] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DistributionDirectCostValues] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionDirectCostValues_DistributionDirectCostValues] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [InteropCost].[DistributionDirectCost] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostValues_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de distribución de costo directo por ventas (DECIMAL 18,2). Factor de prorrateo de costos directos según ingresos o facturación de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por ventas', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de distribución de costo directo por activos (DECIMAL 18,2). Factor de prorrateo de costos directos según valor de bienes, equipos e infraestructura del centro.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por valor de activo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de distribución de costo directo por mano de obra (DECIMAL 18,2). Factor de prorrateo de costos directos según horas-hombre, salarios o dedicación del personal sanitario.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByWorkmanship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de distribución de costo directo por oferta/suministros (DECIMAL 18,2). Factor de prorrateo de costos directos según consumo de materiales, medicamentos o insumos hospitalarios.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por valor de oferta', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'BySupplyValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de distribución de costo directo por horas oficiales (DECIMAL 18,2). Factor de prorrateo de costos directos según jornada laboral, turnos o disponibilidad de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por horas oficiales', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByOfficialHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de distribución de costo directo por área/superficie (DECIMAL 18,2). Factor de prorrateo de costos directos según metros cuadrados o espacio físico ocupado por el centro de producción.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Por área', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ByArea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional o centro de producción (FK → ProductionCenter.Id). Refiere el centro de costo, departamento, servicio o punto de atención al que aplican los valores.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de producción  al que pertenecen los valores', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o documento maestro de distribución de costos directos (FK → DistributionDirectCost.Id). Agrupa el conjunto de valores de prorrateo de un período contable.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del documento de distribución al que pertenece los valores', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'DistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de valores de distribución (INT IDENTITY, PK). Clave primaria que identifica cada línea de asignación de costos en la tabla.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificación del registro', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores de distribución de costos directos por centro de producción. Registra los montos distribuidos según diferentes criterios de prorrateo: área física, horas oficiales, valor de insumos, mano de obra, valor de activos y ventas; utilizados en el proceso de costeo hospitalario.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionDirectCostValues';
