CREATE TABLE [Cost].[CostDistributionSecondaryBaseDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT             NOT NULL,
    [ProductionCenterId]          INT             NOT NULL,
    [Quantity]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryBaseDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostDistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [Cost].[CostDistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostDistributionSecondaryBaseDetail]
    ON [Cost].[CostDistributionSecondaryBaseDetail]([DistributionSecondaryBaseId] ASC, [ProductionCenterId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (NUMERIC 18,2) a distribuir en el centro de producción. Se completa solo cuando el tipo de distribución es calculada; representa el volumen, unidades o porcentaje de asignación de costos secundarios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del centro de producción, unidad funcional o área de costo receptora de la distribución secundaria de costos. Referencia a [Cost].[CostProductionCenter].', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la base de distribución secundaria padre que agrupa reglas de prorrateo de costos indirectos. Referencia a [Cost].[CostDistributionSecondaryBase].', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) de cada detalle de distribución secundaria; clave primaria de la tabla [Cost].[CostDistributionSecondaryBaseDetail].', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las unidades de producción asignadas a cada base secundaria de distribución de costos. Registra qué centros de producción participan en una distribución secundaria y con qué cantidad o volumen de actividad se les asigna el costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryBaseDetail';
