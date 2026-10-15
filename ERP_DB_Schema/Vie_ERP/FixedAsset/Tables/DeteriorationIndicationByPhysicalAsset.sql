CREATE TABLE [FixedAsset].[DeteriorationIndicationByPhysicalAsset] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DeteriorationIndicationId] INT NOT NULL,
    [PhysicalAssetId]           INT NOT NULL,
    CONSTRAINT [PK_DeteriorationIndicationByPhysicalAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeteriorationIndicationByPhysicalAsset_DeteriorationIndication] FOREIGN KEY ([DeteriorationIndicationId]) REFERENCES [FixedAsset].[DeteriorationIndications] ([Id]),
    CONSTRAINT [FK_DeteriorationIndicationByPhysicalAsset_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);


GO
ALTER TABLE [FixedAsset].[DeteriorationIndicationByPhysicalAsset] NOCHECK CONSTRAINT [FK_DeteriorationIndicationByPhysicalAsset_DeteriorationIndication];


GO
ALTER TABLE [FixedAsset].[DeteriorationIndicationByPhysicalAsset] NOCHECK CONSTRAINT [FK_DeteriorationIndicationByPhysicalAsset_FixedAssetPhysicalAsset];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del indicio de deterioro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'DeteriorationIndicationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'DeteriorationIndicationByPhysicalAsset', @level2type = N'COLUMN', @level2name = N'Id';

