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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del activo dijo  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetChangePlateDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';

