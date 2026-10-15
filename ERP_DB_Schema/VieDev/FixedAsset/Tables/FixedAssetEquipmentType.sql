CREATE TABLE [FixedAsset].[FixedAssetEquipmentType] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdParent]         INT          NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [IdInventoryType]  INT          NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEquipmentType_FixedAssetEquipmentType] FOREIGN KEY ([IdParent]) REFERENCES [FixedAsset].[FixedAssetEquipmentType] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentType_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEquipmentType] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentType_FixedAssetEquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetEquipmentType] NOCHECK CONSTRAINT [FK_FixedAssetEquipmentType_FixedAssetInventoryType];

