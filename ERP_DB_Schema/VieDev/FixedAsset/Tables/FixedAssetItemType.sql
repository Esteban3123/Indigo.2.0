CREATE TABLE [FixedAsset].[FixedAssetItemType] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParentId]         INT          NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [InventoryTypeId]  INT          NOT NULL,
    [Status]           BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_EquipmentType_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentType_EquipmentType] FOREIGN KEY ([ParentId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_EquipmentType_InventoryType] FOREIGN KEY ([InventoryTypeId]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemType] NOCHECK CONSTRAINT [FK_EquipmentType_EquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemType] NOCHECK CONSTRAINT [FK_EquipmentType_InventoryType];

