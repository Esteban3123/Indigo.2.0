CREATE TABLE [Inventory].[PurchaseOrderAvailability] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderId]      INT             NOT NULL,
    [AvailabilityDetailId] INT             NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_PurchaseOrderAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_PurchaseOrderAvailability_PurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [Inventory].[PurchaseOrder] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseOrderAvailability] NOCHECK CONSTRAINT [FK_PurchaseOrderAvailability_AvailabilityDetail];


GO
ALTER TABLE [Inventory].[PurchaseOrderAvailability] NOCHECK CONSTRAINT [FK_PurchaseOrderAvailability_PurchaseOrder];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Id';

