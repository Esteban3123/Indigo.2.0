CREATE TABLE [MixingStation].[CampaignDetailPicking] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId] INT          NOT NULL,
    [ProductId]        INT          NOT NULL,
    [WarehouseId]      INT          NOT NULL,
    [BatchSerialId]    INT          NULL,
    [Quantity]         INT          NOT NULL,
    [ItemType]         TINYINT      CONSTRAINT [DF_CampaignDetailPicking_ItemType] DEFAULT ((3)) NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_CampaignDetailPicking] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailPicking_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_CampaignDetailPicking_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_CampaignDetailPicking_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignDetailPicking_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetailPicking] NOCHECK CONSTRAINT [FK_CampaignDetailPicking_BatchSerial];


GO
ALTER TABLE [MixingStation].[CampaignDetailPicking] NOCHECK CONSTRAINT [FK_CampaignDetailPicking_CampaignDetail];


GO
ALTER TABLE [MixingStation].[CampaignDetailPicking] NOCHECK CONSTRAINT [FK_CampaignDetailPicking_InventoryProduct];


GO
ALTER TABLE [MixingStation].[CampaignDetailPicking] NOCHECK CONSTRAINT [FK_CampaignDetailPicking_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Pricipal  2 - Canasta  3 - Otro  4 - Solicitudes Manuales  ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailPicking', @level2type = N'COLUMN', @level2name = N'ItemType';

