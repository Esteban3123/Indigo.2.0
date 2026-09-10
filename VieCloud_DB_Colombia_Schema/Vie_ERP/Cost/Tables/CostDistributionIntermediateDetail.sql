CREATE TABLE [Cost].[CostDistributionIntermediateDetail] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionIntermediateId]       INT             NOT NULL,
    [ProductionCenterId]               INT             NOT NULL,
    [Proportion]                       NUMERIC (5, 2)  NOT NULL,
    [Value]                            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionIntermediateDetail_Value] DEFAULT ((0)) NOT NULL,
    [DirectDistributionIntermediateId] INT             NULL,
    CONSTRAINT [PK_CostDistributionIntermediateDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionIntermediateDetail_CostDirectDistributionIntermediate] FOREIGN KEY ([DirectDistributionIntermediateId]) REFERENCES [Cost].[CostDirectDistributionIntermediate] ([Id]),
    CONSTRAINT [FK_CostDistributionIntermediateDetail_CostDistributionIntermediate] FOREIGN KEY ([DistributionIntermediateId]) REFERENCES [Cost].[CostDistributionIntermediate] ([Id]),
    CONSTRAINT [FK_CostDistributionIntermediateDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) de la distribución de costos asignado al centro de producción en el detalle intermedio. Importe, monto, cantidad económica distribuida.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de proporción (NUMERIC 5,2) que se distribuye del costo intermedio hacia el centro de producción. Tasa, ratio, porcentaje de asignación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de proporcion que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del centro de producción, unidad funcional o área que recibe la distribución de costos. Referencia a Cost.CostProductionCenter.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cabecera o registro padre de distribución intermedia de costos. Referencia a Cost.CostDistributionIntermediate.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle o línea de distribución intermedia de costos. Clave primaria del desglose.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_CostDistributionIntermediateDetail_DirectDistributionId]
    ON [Cost].[CostDistributionIntermediateDetail]([DirectDistributionIntermediateId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la transacción de distribución intermedia ejecutada (CostDirectDistributionIntermediate)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DirectDistributionIntermediateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de distribución intermedia de costos por centro de producción. Registra la proporción y el valor asignado a cada centro de producción dentro de un proceso de distribución intermedia de costos hospitalarios o administrativos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionIntermediateDetail';
