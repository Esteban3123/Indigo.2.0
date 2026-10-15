CREATE TABLE [MixingStation].[CampaignDetailBasketDetail] (
    [Id]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]  INT             NOT NULL,
    [ComponentType]     TINYINT         NOT NULL,
    [ProductId]         INT             NULL,
    [AtcId]             INT             NULL,
    [SupplieId]         INT             NULL,
    [Quantity]          DECIMAL (18, 6) NOT NULL,
    [MeasurementUnitId] INT             NULL,
    CONSTRAINT [PK_CampaignDetailBasketDeatail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignDetailBasketDeatail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignDetailBasketDetail] NOCHECK CONSTRAINT [FK_CampaignDetailBasketDeatail_ATC];


GO
ALTER TABLE [MixingStation].[CampaignDetailBasketDetail] NOCHECK CONSTRAINT [FK_CampaignDetailBasketDeatail_InventoryMeasurementUnit];


GO
ALTER TABLE [MixingStation].[CampaignDetailBasketDetail] NOCHECK CONSTRAINT [FK_CampaignDetailBasketDeatail_InventoryProduct];


GO
ALTER TABLE [MixingStation].[CampaignDetailBasketDetail] NOCHECK CONSTRAINT [FK_CampaignDetailBasketDeatail_InventorySupplie];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de componente:   1: Medicamento  2: Insumo  3: Producto de tipo otro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignDetailBasketDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';

