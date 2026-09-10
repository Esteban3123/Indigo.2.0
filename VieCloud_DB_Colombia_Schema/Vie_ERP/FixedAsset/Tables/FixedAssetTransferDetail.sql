CREATE TABLE [FixedAsset].[FixedAssetTransferDetail] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetTransferId] INT NOT NULL,
    [PhysicalAssetId]      INT NOT NULL,
    CONSTRAINT [PK_FixedAssetTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetTransferDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetTransferDetail_FixedAssetTransfer] FOREIGN KEY ([FixedAssetTransferId]) REFERENCES [FixedAsset].[FixedAssetTransfer] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del activo fijo físico a transferir. Referencia a FixedAssetPhysicalAsset. Requerido cuando el tipo de detalle corresponde a activos fijos. INT, FK obligatoria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Activo Fijo, este debe ir definido si el tipo de detalle es Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la transferencia de activo fijo padre. Referencia a FixedAssetTransfer. Vincula cada línea de detalle con su movimiento de transferencia. INT, FK obligatoria.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de transferencia de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la línea de detalle de transferencia. Primary key de la tabla. INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de transferencias de activos fijos: registra cada activo físico incluido en una orden de traslado o reasignación de bienes entre áreas, sedes o responsables.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail';
