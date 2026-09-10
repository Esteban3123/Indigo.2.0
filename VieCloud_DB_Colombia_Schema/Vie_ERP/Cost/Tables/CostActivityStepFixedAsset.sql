CREATE TABLE [Cost].[CostActivityStepFixedAsset] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId] INT             NOT NULL,
    [FixedAssetItemId]   INT             NOT NULL,
    [Hours]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepFixedAsset__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepFixedAsset_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepFixedAsset_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStepFixedAsset__CostActivityStepId__FixedAssetItemId]
    ON [Cost].[CostActivityStepFixedAsset]([CostActivityStepId] ASC, [FixedAssetItemId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horas requeridas para la ejecución del paso de actividad en el activo fijo; tipo DECIMAL(24,6) para precisión en cálculos de costos y depreciación horaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Hours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas requeridas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Hours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del artículo de activos fijos (FK); referencia a FixedAsset.FixedAssetItem; equipo, máquina, inmueble o bien asignado al paso de la actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Artículo de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paso de la actividad de costo (FK); referencia a Cost.CostActivityStep; vincula a la etapa específica del proceso de costeo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Paso de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'CostActivityStepId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de relación entre paso de actividad y activo fijo; clave primaria IDENTITY(1,1)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los activos fijos (equipos, maquinaria, infraestructura) asociados a cada paso de una actividad de costos, incluyendo las horas de uso de dicho activo en ese paso del proceso.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStepFixedAsset';
