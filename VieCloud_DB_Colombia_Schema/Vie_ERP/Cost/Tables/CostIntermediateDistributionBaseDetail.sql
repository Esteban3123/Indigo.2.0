CREATE TABLE [Cost].[CostIntermediateDistributionBaseDetail] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IntermediateDistributionBaseId] INT             NOT NULL,
    [ProductionCenterId]             INT             NOT NULL,
    [Quantity]                       NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_CostIntermediateDistributionBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostIntermediateDistributionBaseDetail_CostIntermediateDistributionBase] FOREIGN KEY ([IntermediateDistributionBaseId]) REFERENCES [Cost].[CostIntermediateDistributionBase] ([Id]),
    CONSTRAINT [FK_CostIntermediateDistributionBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (18,2) a distribuir en costos intermedios; se completa solo cuando el tipo de distribución es calculada. Representa el volumen, unidades o proporción de asignación de gastos del centro de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del centro de producción, unidad funcional o departamento destino que recibe la distribución de costos intermedios. Referencia a Cost.CostProductionCenter.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la base de distribución intermedia que agrupa los criterios y detalles de asignación de costos entre centros productivos. Referencia a Cost.CostIntermediateDistributionBase.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK clustered, IDENTITY 1,1) del detalle de distribución intermedia de costos. Llave primaria de la tabla CostIntermediateDistributionBaseDetail.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las bases de distribución intermedia de costos: registra la cantidad asignada a cada centro de producción dentro de una base de distribución, permitiendo prorratear costos indirectos entre las unidades funcionales o centros de costo intermedios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionBaseDetail';
