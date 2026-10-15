CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderAvailability] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetPurchaseOrderId] INT             NOT NULL,
    [AvailabilityDetailId]      INT             NOT NULL,
    [Value]                     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_FixedAssetPurchaseOrder] FOREIGN KEY ([FixedAssetPurchaseOrderId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrder] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderAvailability] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_AvailabilityDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderAvailability] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_FixedAssetPurchaseOrder];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'FixedAssetPurchaseOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetPurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Id';

