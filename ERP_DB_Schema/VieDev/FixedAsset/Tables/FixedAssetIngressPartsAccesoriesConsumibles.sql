CREATE TABLE [FixedAsset].[FixedAssetIngressPartsAccesoriesConsumibles] (
    [Id]                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngressEquipment] INT          NOT NULL,
    [IdEquipment]                  INT          NOT NULL,
    [IdPartsAccesoriesConsumibles] INT          NOT NULL,
    [DepreciatePart]               BIT          NULL,
    [Value]                        NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetIngressPartsAccesoriesConsumibles_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetEquipment] FOREIGN KEY ([IdEquipment]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressPartsAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetEquipment];

