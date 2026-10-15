CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetParts] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId]             INT             NOT NULL,
    [PartAccesoriesConsumiblesId] INT             NOT NULL,
    [DepreciatePart]              BIT             NOT NULL,
    [HistoricalValue]             DECIMAL (18, 2) NOT NULL,
    [HasOutput]                   BIT             CONSTRAINT [DF_FixedAssetPhysicalAssetParts_HasOutput] DEFAULT ((0)) NOT NULL,
    [OutputDate]                  DATE            NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetParts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAssetParts_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_FixedAssetPhysicalAssetParts_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetParts] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetParts_FixedAssetPartsAccesoriesConsumables];


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetParts] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetParts_FixedAssetPhysicalAsset];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la parte del activo agregado ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del activo fijo ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las partes del Activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPhysicalAssetParts', @level2type = N'COLUMN', @level2name = N'Id';

