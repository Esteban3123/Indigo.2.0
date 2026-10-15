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

