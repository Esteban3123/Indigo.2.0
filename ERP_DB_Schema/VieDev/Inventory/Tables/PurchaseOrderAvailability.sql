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

