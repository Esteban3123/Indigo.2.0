CREATE TABLE [FixedAsset].[FixedAssetEntryCommitment] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryId]  INT             NOT NULL,
    [CommitmentDetailId] INT             NOT NULL,
    [Value]              DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetEntryCommitment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryCommitment_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryCommitment_FixedAssetEntry] FOREIGN KEY ([FixedAssetEntryId]) REFERENCES [FixedAsset].[FixedAssetEntry] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) que se asignará como obligación presupuestaria en el detalle del compromiso seleccionado; importe que vincula el activo fijo al compromiso de pago.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se generará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del compromiso presupuestario o línea de obligación en Budget.CommitmentDetail; referencia a la línea específica del compromiso que será afectada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera o registro principal del ingreso de activo fijo en FixedAsset.FixedAssetEntry; llave que vincula el compromiso al activo registrado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del ingreso de activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de vinculación entre activo fijo y compromiso presupuestario; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los compromisos presupuestales asociados a cada entrada o incorporación de activos fijos, vinculando el movimiento contable de ingreso del activo con el detalle del compromiso presupuestal y el valor comprometido.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryCommitment';
