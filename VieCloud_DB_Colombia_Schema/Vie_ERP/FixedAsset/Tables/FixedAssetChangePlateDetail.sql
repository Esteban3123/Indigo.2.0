CREATE TABLE [FixedAsset].[FixedAssetChangePlateDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetChangePlateId]   INT          NOT NULL,
    [FixedAssetPhysicalAssetId] INT          NOT NULL,
    [OldPlate]                  VARCHAR (50) NOT NULL,
    [NewPlate]                  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FixedAssetChangePlateDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetChangePlateDetail_FixedAssetChangePlate] FOREIGN KEY ([FixedAssetChangePlateId]) REFERENCES [FixedAsset].[FixedAssetChangePlate] ([Id]),
    CONSTRAINT [FK_FixedAssetChangePlateDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa nueva o identificador de matrícula actual asignada al activo fijo después del cambio de placa (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'NewPlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa nueva', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'NewPlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'NewPlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa antigua o identificador de matrícula anterior del activo fijo antes del cambio de placa (VARCHAR 50)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'OldPlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placa vieja', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'OldPlate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'OldPlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del activo físico cuya placa se está modificando, referencia a FixedAssetPhysicalAsset (FK INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del activo dijo  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la transacción o solicitud de cambio de placa del activo fijo, referencia a FixedAssetChangePlate (FK INT)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetChangePlateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de placa de cambio de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetChangePlateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetChangePlateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (IDENTITY) de cada detalle de cambio de placa registrado (INT PK)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los cambios de placa (número de identificación físico) realizados sobre activos fijos. Registra el historial de cambios de placa por activo, indicando la placa anterior y la nueva placa asignada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail';
