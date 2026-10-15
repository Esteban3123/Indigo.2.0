CREATE TABLE [MixingStation].[CampaignDetailValidation] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]      INT          NOT NULL,
    [ProductId]             INT          NOT NULL,
    [WarehouseId]           INT          NOT NULL,
    [BatchSerialId]         INT          NULL,
    [DeliveredQuantity]     INT          NOT NULL,
    [DevolutionQuantity]    INT          CONSTRAINT [DF_CampaignDetailValidation_RefundQuantity] DEFAULT ((0)) NOT NULL,
    [TransferOrderQuantity] INT          CONSTRAINT [DF_CampaignDetailValidation_TransferOrderQuantity] DEFAULT ((0)) NOT NULL,
    [ItemType]              TINYINT      CONSTRAINT [DF_CampaignDetailValidation_ItemType] DEFAULT ((3)) NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    CONSTRAINT [PK_CampaignDetailValidation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailValidation_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_CampaignDetailValidation_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_CampaignDetailValidation_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignDetailValidation_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetailValidation] NOCHECK CONSTRAINT [FK_CampaignDetailValidation_BatchSerial];


GO
ALTER TABLE [MixingStation].[CampaignDetailValidation] NOCHECK CONSTRAINT [FK_CampaignDetailValidation_CampaignDetail];


GO
ALTER TABLE [MixingStation].[CampaignDetailValidation] NOCHECK CONSTRAINT [FK_CampaignDetailValidation_InventoryProduct];


GO
ALTER TABLE [MixingStation].[CampaignDetailValidation] NOCHECK CONSTRAINT [FK_CampaignDetailValidation_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Pricipal  2 - Canasta  3 - Otro  4 - Solicitudes Manuales', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailValidation', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad enviada Orden de traslado al momento Guardar y Confirmar. ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailValidation', @level2type = N'COLUMN', @level2name = N'TransferOrderQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Devuelta: Solo es afectada cuando se genere una devolución de materia prima.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailValidation', @level2type = N'COLUMN', @level2name = N'DevolutionQuantity';

