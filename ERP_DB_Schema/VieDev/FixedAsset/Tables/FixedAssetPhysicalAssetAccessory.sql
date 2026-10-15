CREATE TABLE [FixedAsset].[FixedAssetPhysicalAssetAccessory] (
    [Id]              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PhysicalAssetId] INT             NOT NULL,
    [Description]     VARCHAR (500)   NOT NULL,
    [Serie]           VARCHAR (50)    NULL,
    [Quantity]        INT             NOT NULL,
    [Value]           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetPhysicalAssetAccessory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPhysicalAssetAccessory_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPhysicalAssetAccessory] NOCHECK CONSTRAINT [FK_FixedAssetPhysicalAssetAccessory_FixedAssetPhysicalAsset];

