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
ALTER TABLE [FixedAsset].[FixedAssetChangePlateDetail] NOCHECK CONSTRAINT [FK_FixedAssetChangePlateDetail_FixedAssetChangePlate];


GO
ALTER TABLE [FixedAsset].[FixedAssetChangePlateDetail] NOCHECK CONSTRAINT [FK_FixedAssetChangePlateDetail_FixedAssetPhysicalAsset];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placa nueva', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'NewPlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placa vieja', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'OldPlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del activo dijo  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de placa de cambio de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetChangePlateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'Id';

