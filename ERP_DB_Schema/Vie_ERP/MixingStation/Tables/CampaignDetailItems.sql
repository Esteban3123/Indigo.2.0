CREATE TABLE [MixingStation].[CampaignDetailItems] (
    [Id]                                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]                        INT           NOT NULL,
    [AtcId]                                   INT           NULL,
    [SupplyId]                                INT           NULL,
    [ProductId]                               INT           NULL,
    [RequestQuantity]                         INT           NOT NULL,
    [QuantityStock]                           INT           NOT NULL,
    [QuantityWarehouse]                       INT           NOT NULL,
    [QuantityMaquila]                         INT           CONSTRAINT [DF_CampaignDetailItems_QuantityMaquila] DEFAULT ((0)) NOT NULL,
    [ItemType]                                TINYINT       CONSTRAINT [DF_CampaignDetailItems_ItemType] DEFAULT ((3)) NOT NULL,
    [QuantityRemnant]                         INT           CONSTRAINT [DF__CampaignD__Remna__00B07205] DEFAULT ((0)) NOT NULL,
    [CauseReprocessingRejectionId]            INT           NULL,
    [CauseReprocessingRejectionJustification] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CampaignDetailItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailItems_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_CauseReprocessingRejection] FOREIGN KEY ([CauseReprocessingRejectionId]) REFERENCES [MixingStation].[CauseReprocessingRejection] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignDetailItems_InventorySupplie] FOREIGN KEY ([SupplyId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetailItems] NOCHECK CONSTRAINT [FK_CampaignDetailItems_ATC];


GO
ALTER TABLE [MixingStation].[CampaignDetailItems] NOCHECK CONSTRAINT [FK_CampaignDetailItems_CampaignDetail];


GO
ALTER TABLE [MixingStation].[CampaignDetailItems] NOCHECK CONSTRAINT [FK_CampaignDetailItems_CauseReprocessingRejection];


GO
ALTER TABLE [MixingStation].[CampaignDetailItems] NOCHECK CONSTRAINT [FK_CampaignDetailItems_InventoryProduct];


GO
ALTER TABLE [MixingStation].[CampaignDetailItems] NOCHECK CONSTRAINT [FK_CampaignDetailItems_InventorySupplie];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Remanentes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'QuantityRemnant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Pricipal  2 - Canasta  3 - Otro  4 - Solicitudes Manuales  ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailItems', @level2type = N'COLUMN', @level2name = N'ItemType';

