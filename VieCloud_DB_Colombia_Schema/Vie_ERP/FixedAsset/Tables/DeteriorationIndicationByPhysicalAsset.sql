CREATE TABLE [FixedAsset].[DeteriorationIndicationByPhysicalAsset] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DeteriorationIndicationId] INT NOT NULL,
    [PhysicalAssetId]           INT NOT NULL,
    CONSTRAINT [PK_DeteriorationIndicationByPhysicalAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeteriorationIndicationByPhysicalAsset_DeteriorationIndication] FOREIGN KEY ([DeteriorationIndicationId]) REFERENCES [FixedAsset].[DeteriorationIndications] ([Id]),
    CONSTRAINT [FK_DeteriorationIndicationByPhysicalAsset_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del activo fijo físico (equipo médico, maquinaria, inmueble). Clave foránea a FixedAssetPhysicalAsset. INT, requerido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del indicio o evidencia de deterioro del activo (desgaste, daño, obsolescencia, falla). Clave foránea a DeteriorationIndications. INT, requerido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'DeteriorationIndicationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del indicio de deterioro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'DeteriorationIndicationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'DeteriorationIndicationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la asociación entre activo fijo y su indicio de deterioro. Clave primaria, INT IDENTITY, auto-incremental.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los indicios o señales de deterioro detectados con cada activo fijo físico específico. Permite registrar qué tipos de deterioro aplican a un bien determinado para su evaluación y seguimiento contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset';
